using System.IO;
using System.Reflection;
using UnityEngine;

namespace ExpandTheGungeon.ExpandAudio {

	public class AudioResourceLoader {


        public static readonly string ResourcesDirectoryName = "ExpandTheGungeon";
        public static readonly string AutoprocessDirectoryName = "Mods/ExpandTheGungeon";
        public static readonly string AutoprocessModPathName = "Mods/ExpandTheGungeon";
        public static readonly string ResourcesAutoprocessDirectoryName = AutoprocessDirectoryName;

        public static readonly string FullPathGame = Path.Combine(Application.dataPath, "..");
        
        public static readonly string FullPathResources = FullPathGame;
        public static readonly string FullPathAutoprocess = Path.Combine(FullPathResources, AutoprocessDirectoryName);
        

        public static void InitAudio() {
            // LoadAllAutoloadResourcesFromModPath(AutoprocessModPathName);
            LoadAllAutoloadResourcesFromAssembly(Assembly.GetExecutingAssembly(), "ExpandTheGungeon");

            // LoadAllAutoloadResourcesFromPath(FullPathAutoprocess, "ExpandTheGungeon");
        }

		public static void LoadAllAutoloadResourcesFromAssembly(Assembly assembly, string prefix) {
            // this.LoaderText.AutoloadFromAssembly(assembly, prefix);
            // this.LoaderSprites.AutoloadFromAssembly(assembly, prefix, textureSize);
            ResourceLoaderSoundbanks LoaderSoundbanks = new ResourceLoaderSoundbanks();
            LoaderSoundbanks.AutoloadFromAssembly(assembly, prefix);
		}
        
		/*public static void LoadAllAutoloadResourcesFromPath(string path, string prefix) {
            // this.LoaderText.AutoloadFromPath(path, prefix);
            // this.LoaderSprites.AutoloadFromPath(path, prefix, textureSize);
            ResourceLoaderSoundbanks LoaderSoundbanks = new ResourceLoaderSoundbanks();
            LoaderSoundbanks.AutoloadFromPath(path, prefix);
		}

        public static void LoadAllAutoloadResourcesFromModPath(string path) {
            ResourceLoaderSoundbanks LoaderSoundbanks = new ResourceLoaderSoundbanks();
            LoaderSoundbanks.AutoloadFromModZIPOrModFolder(path);
        }*/

	}
}
