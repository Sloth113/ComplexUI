using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(MyButton))]
public class MyButtonEditor : UnityEditor.UI.ButtonEditor {
    public override void OnInspectorGUI()
    {
        MyButton button = (MyButton)target;

        base.OnInspectorGUI();

        //button.m_timer = EditorGUI
    }

}
