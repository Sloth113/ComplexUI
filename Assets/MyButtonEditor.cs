    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MyButton),true)]
public class MyButtonEditor : UnityEditor.UI.ButtonEditor {
    public override void OnInspectorGUI()
    {
     
        MyButton button = (MyButton)target;
        
        base.OnInspectorGUI();
        button.m_timer = EditorGUILayout.FloatField("Test: ", 100000);
    }

}
