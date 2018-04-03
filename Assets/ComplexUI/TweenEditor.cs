using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//Currently does nothing. 
//[CustomEditor(typeof(Tween), true)]
public class TweenEditor : Editor {

    public override void OnInspectorGUI()
    {
        
        Tween item = (Tween)target;
        
        base.OnInspectorGUI();


        EditorUtility.SetDirty(target);
    }
}
