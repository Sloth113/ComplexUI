using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CUI
{
    //Used to clear ICustom tweens 
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
                if(tween != this)
                    tween.Clear();
            }
            Destroy(this);
        }

    }
}
