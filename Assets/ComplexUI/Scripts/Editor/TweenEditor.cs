using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace CUI
{
    //Currently does nothing. 
    //[CustomEditor(typeof(Tween), true)]
    //Was going to be used for tweens
    public class TweenEditor : Editor
    {

        public override void OnInspectorGUI()
        {

            Tween item = (Tween)target;

            base.OnInspectorGUI();


            EditorUtility.SetDirty(target);
        }
    }
}
