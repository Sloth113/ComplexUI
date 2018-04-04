using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CUI
{
    //Applies multi tweens to an object on the apply call
    [CreateAssetMenu(fileName = "Tween", menuName = "ComplexUI/Tween/Composite Tween", order = 0)]
    public class CompositeTween : Tween
    {
        [SerializeField] private Tween[] m_tweens;
        [SerializeField] private float m_delayBetween;
        public override void Apply(GameObject obj)
        {
            Apply(obj, 0);
        }

        public override void Apply(GameObject obj, float delay)
        {
            float totalDelay = 0;
            foreach (Tween t in m_tweens)
            {
                if (t != null)
                    t.Apply(obj, totalDelay);
                totalDelay += delay;
            }
        }
        //Different
        public override float GetDelay()
        {
            return m_delayBetween;
        }

        public override float GetTime()
        {
            float time = 0;
            foreach (Tween t in m_tweens)
            {
                time += t.GetTime();
            }
            return time;
        }
    }
}
