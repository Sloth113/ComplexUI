    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

[CustomEditor(typeof(MyButton),true)]
public class MyButtonEditor : UnityEditor.UI.ButtonEditor {
    public override void OnInspectorGUI()
    {        
        MyButton button = (MyButton)target;
        //DrawDefaultInspector();
        base.OnInspectorGUI();

        serializedObject.Update();

        EditorGUILayout.PropertyField(serializedObject.FindProperty("m_overTweens"), EditorStyles.standardFont);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("m_exitTweens"), EditorStyles.standardFont);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("m_downTweens"), EditorStyles.standardFont);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("m_upTweens"), EditorStyles.standardFont);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("m_enabledTweens"), EditorStyles.standardFont);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("m_disabledTweens"), EditorStyles.standardFont);
        //Event tweens?

        serializedObject.ApplyModifiedProperties();
    }

}
