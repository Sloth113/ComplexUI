using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Tween))]
public class TweenEditor : Editor {

    public override void OnInspectorGUI()
    {
        uint i = 0;
        Tween item = (Tween)target;

        if(item.m_type == Tween.Type.ShakeRot)
        {
            GUILayout.Label("THIS IS A SHAKE THING!");
        }
        else
        {
            //base.OnInspectorGUI();
        }

        base.OnInspectorGUI();


        EditorUtility.SetDirty(target);
    }
}
