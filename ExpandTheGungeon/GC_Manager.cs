using Dungeonator;
using MonoMod.RuntimeDetour;
using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;
using UnityEngine;

namespace ExpandTheGungeon {

    public class GC_Manager : MonoBehaviour {

        public static GC_Manager Instance {
            get {
                if (!m_instance) { m_instance = ETGModMainBehaviour.Instance.gameObject.AddComponent<GC_Manager>(); }
                return m_instance;
            }
        }

        private static GC_Manager m_instance;
        
        public GC_Manager() {
            turn_off_mono_gc = false;
            manual_gc_profile = false;

            manual_gc_factor_threshold = 2;
            manual_gc_min_time_delta_seconds = 10;

            expected_time_until_gc = -1;

            average_allocation_rate_mbps = -1;
            allocated_mb = 0;
            manual_gc_most_recent_in_use_bytes = -1;
            last_gc_time = -1;
            last_gc_allocated_mb = -1;

            Init();
        }

        public static Hook BraveMemoryCollectHook;
        
        private static FieldInfo LastGcTime = typeof(BraveMemory).GetField("LastGcTime", BindingFlags.Static | BindingFlags.NonPublic);

        // Extracted from mono.pdb using dbh.exe (using the "enum *!*mono_gc_*" command)
        // Note: for the 64 bit editor, there is only a 64 bit version of mono.pdb, so you need to also download the 32 bit editor to update this for 32 bit standalone builds
        // (you also need to decide which version of the dll to use; this can be done by comparing the mono_gc_collect offset with the two offsets for the 32 bit and 64 bit dlls)
        // Apache - Updated offsets for the version of Mono Enter The Gungeon uses
        public static int offset_mono_gc_disable = 0x1b310;
        public static int offset_mono_gc_enable = 0x1b318;
        public static int offset_mono_gc_collect = 0x1b2c4;


        public static int manual_gc_bytes_threshold_mb = 1024;

        public class force_enable_gc_token { public int count; };

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("kernel32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

        public static Action mono_gc_disable;
        public static Action mono_gc_enable;
        public static Action mono_gc_collect;


        //set this to true to have the GC be manually invoked by this script when certain thresholds are reached
        public bool turn_off_mono_gc;

        public bool manual_gc_profile;

        public static bool d_gc_disabled = false;
        private static bool mono_gc_loaded = false;

        
        //set by this script every update. this is the expected number of seconds until gc runs, or -1 if unknown
        //this can be used to run gc early e.g. if the game is paused
        public float expected_time_until_gc;

        private float manual_gc_factor_threshold;
        private float manual_gc_min_time_delta_seconds;

        private float average_allocation_rate_mbps;
        private float allocated_mb;
        private long manual_gc_most_recent_in_use_bytes;
        private float last_gc_time;
        private float last_gc_allocated_mb;

        private int[] dummy_object;


        public static void DoCollect() {
            if (d_gc_disabled && !Dungeon.IsGenerating) { return; }

            if (d_gc_disabled) {
                LastGcTime.SetValue(typeof(BraveMemory), Time.realtimeSinceStartup);
                Instance.Collect();
            } else {
                LastGcTime.SetValue(typeof(BraveMemory), Time.realtimeSinceStartup);
                GC.Collect();
            }
            
        }

        public void ToggleHookAndGC(bool state = true) {
            if (state) {
                Enable();
                if (BraveMemoryCollectHook == null) {
                    new Hook(
                        typeof(BraveMemory).GetMethod(nameof(BraveMemory.DoCollect), BindingFlags.Public | BindingFlags.Static),
                        typeof(GC_Manager).GetMethod(nameof(DoCollect), BindingFlags.Public | BindingFlags.Static)
                    );
                }
                if (SystemInfo.systemMemorySize > 12000) {
                    manual_gc_bytes_threshold_mb = 3000;
                } else if (SystemInfo.systemMemorySize > 6100) {
                    manual_gc_bytes_threshold_mb = 2176;
                } else {
                    manual_gc_bytes_threshold_mb = 1280;
                }
            }
            else {
                if (BraveMemoryCollectHook != null) { BraveMemoryCollectHook.Dispose(); BraveMemoryCollectHook = null; }
                Disable();
            }
        }

        public static bool load_mono_gc() {

            if (mono_gc_loaded) { return true; }
            
            IntPtr mono_module = GetModuleHandle("mono.dll");
            IntPtr func_ptr_mono_gc_collect = new IntPtr(mono_module.ToInt64() + offset_mono_gc_collect);
            IntPtr expected_func_ptr_mono_gc_collect = GetProcAddress(mono_module, "mono_gc_collect");
            if (func_ptr_mono_gc_collect != expected_func_ptr_mono_gc_collect) {
                //if you see this error, you need to update the "offset_mono_gc_" variables defined near the top of this class.
                ETGModConsole.Log("[ExpandTheGungeon] Cannot load gc functions. Expected collect at " + func_ptr_mono_gc_collect.ToInt64() + " Actual at " + func_ptr_mono_gc_collect.ToInt64() + " Module root " + mono_module.ToInt64(), true);
                return false;
            }

            mono_gc_enable = (Action)Marshal.GetDelegateForFunctionPointer(new IntPtr(mono_module.ToInt64() + offset_mono_gc_enable), typeof(Action));
            mono_gc_disable = (Action)Marshal.GetDelegateForFunctionPointer(new IntPtr(mono_module.ToInt64() + offset_mono_gc_disable), typeof(Action));
            mono_gc_collect = (Action)Marshal.GetDelegateForFunctionPointer(new IntPtr(mono_module.ToInt64() + offset_mono_gc_collect), typeof(Action));

            mono_gc_loaded = true;
            return true;
        }

        public void Init() {
            if (!load_mono_gc()) { turn_off_mono_gc = false; }
            
            if (turn_off_mono_gc) {
                mono_gc_disable();
                d_gc_disabled = true;
                GameManager.Instance.StartCoroutine(run_manual_gc_after(0.1f)); // to get average_allocation_rate_mbps to work
            }
        }

        public void Disable() {
            if (load_mono_gc() && turn_off_mono_gc) {
                mono_gc_enable();
                d_gc_disabled = false;
            }
        }

        public void Enable() {
            if (load_mono_gc() && !turn_off_mono_gc) {
                mono_gc_disable();
                d_gc_disabled = true;
            }
        }
        
        private void Start() { }

        protected void FixedUpdate() { monitor_gc(); }

        protected void Update() { monitor_gc(); }

        private IEnumerator run_manual_gc_after(float time) {
            yield return new WaitForSeconds(time);
            manual_gc();
        }

        public void Collect() {
            if (d_gc_disabled) { manual_gc(); } else { GC.Collect(); }
        }
        
        public void manual_gc() {
            // assert._(d_gc_disabled);

            float start_time = (manual_gc_profile) ? Time.realtimeSinceStartup : 0;
            float bytes_allocated_initially = (manual_gc_profile) ? GC.GetTotalMemory(false) : 0;

            int collection_count = GC.CollectionCount(0);
            mono_gc_enable();

            //see if gc will run on its own after being enabled
            for (int x = 0; x < 100; ++x) {
                dummy_object = new int[1];
                dummy_object[0] = 0;
            }
            int new_collection_count = GC.CollectionCount(0);
            if (new_collection_count == collection_count) {
                // GC.Collect(); //if not, run it manually
                LastGcTime.SetValue(null, Time.realtimeSinceStartup);
                GC.Collect();
            }

            mono_gc_disable();

            manual_gc_most_recent_in_use_bytes = GC.GetTotalMemory(false);

            // Use HUDGC to track this.
            /*if (manual_gc_profile) {
                float end_time = Time.realtimeSinceStartup;
                Debug.Log(
                    "Ran GC iteration.\n" +
                    "Time: " + (end_time - start_time) * 1000 + " ms\n" +
                    "Initial alloc: " + bytes_allocated_initially / 1024 / 1024 + " MB\n" +
                    "Final alloc: " + ((float)manual_gc_most_recent_in_use_bytes) / 1024 / 1024 + " MB\n" +
                    "Util: " + (manual_gc_most_recent_in_use_bytes / bytes_allocated_initially * 100) + " %\n"
                );
            }*/

            allocated_mb = ((float)manual_gc_most_recent_in_use_bytes) / 1024 / 1024;
            last_gc_time = Time.realtimeSinceStartup;
            last_gc_allocated_mb = allocated_mb;
        }

        public void monitor_gc() {
            if (!d_gc_disabled | (GameManager.Instance && GameManager.Instance.IsLoadingLevel)) {
                // enabled = false;
                return;
            }

            long allocated_bytes = GC.GetTotalMemory(false);
            allocated_mb = ((float)allocated_bytes) / 1024 / 1024;

            float allocated_mb_limit = manual_gc_bytes_threshold_mb;
            if (manual_gc_most_recent_in_use_bytes != -1) {
                allocated_mb_limit = Mathf.Max(allocated_mb_limit, ((float)manual_gc_most_recent_in_use_bytes) / 1024 / 1024 * manual_gc_factor_threshold);
            }

            if (allocated_mb >= allocated_mb_limit) {
                manual_gc();
                if (ExpandStats.TrashManSoundFXForCollection) { AkSoundEngine.PostEvent("Play_EX_TrashMan_01", gameObject); }
            }
            float time = Time.realtimeSinceStartup;
            if (last_gc_time != -1) {
                float delta = time - last_gc_time;
                if (delta >= manual_gc_min_time_delta_seconds) {
                    average_allocation_rate_mbps = (allocated_mb - last_gc_allocated_mb) / delta;
                }
            }
            if (average_allocation_rate_mbps != -1) { expected_time_until_gc = (allocated_mb_limit - allocated_mb) / average_allocation_rate_mbps; }
        }
    }
}

