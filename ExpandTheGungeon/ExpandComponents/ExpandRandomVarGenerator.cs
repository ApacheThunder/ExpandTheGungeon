using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandRandomVarGenerator : MonoBehaviour {
        
        public bool GenerateRandomBool() { return BraveUtility.RandomBool(); }

        public float GenerateRandomFloat() { return UnityEngine.Random.value; }

        public int GenerateRandomInt(int min, int max) { return UnityEngine.Random.Range(min, max); }

        public object GenerateRandomItemFromListOrArray(List<object> sourceList = null, object[] sourceArray = null) {
            if (sourceList != null && sourceList.Count > 0) { return BraveUtility.RandomElement(sourceList); }
            if (sourceArray != null && sourceArray.Length > 0) { return BraveUtility.RandomElement(sourceArray); }
            return null;
        }        
    }
}


