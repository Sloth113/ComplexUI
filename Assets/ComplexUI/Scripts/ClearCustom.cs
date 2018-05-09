using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CUI
{
    public class ClearCustom : MonoBehaviour, ICustomTween
    {
        public void Clear()
        {
            Destroy(this);
        }
        
        void Start()
        {
            ICustomTween[] myTweens = GetComponents<ICustomTween>();
            foreach(ICustomTween tween in myTweens)
            {
                tween.Clear();
            }
            Destroy(this);
        }

    }
}
