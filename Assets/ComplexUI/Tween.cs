using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

[System.Serializable]
public abstract class Tween : ScriptableObject {

    public abstract void Apply(GameObject obj);


}
