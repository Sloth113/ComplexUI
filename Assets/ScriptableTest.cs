using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class ScriptableTest : ScriptableObject {

    public float m_speed;
    public float m_time;
    private MyButton m_button;
    public MyButton button
    {
        set
        {
            m_button = value;
        }
        get
        {
            return m_button;
        }
    }
    
    [MenuItem("Assets/Create/FancyUI/ScriptableThing")]
    public static void CreateAsset()
    {
        //ScriptableUtility.CreateAsset<ScriptableTest>("Test");
    }


}
