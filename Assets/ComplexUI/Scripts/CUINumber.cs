using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CUI
{
    [RequireComponent(typeof(Text))]
    public class CUINumber : MonoBehaviour
    {
        [SerializeField] private float m_value;
        [Tooltip("int = 0, float = 0.000")]
        [SerializeField] private string m_format;
        private Text m_text;

        [SerializeField] StateActions m_change;
        // Use this for initialization
        void Start()
        {
            m_text = GetComponent<Text>();
            m_text.text = m_value.ToString(m_format);
        }

        public void ChangeValue(float value)
        {
            m_value += value;
            PlayAction(m_change);
        }
        //Play actions on change.. 
        private float PlayAction(StateActions action)
        {
            float delay = 0;
            foreach (Tween t in action.m_tweens)
            {
                if (t != null)
                    t.Apply(this.gameObject, delay);
                delay += action.m_delay + (action.m_squence ? t.GetDuration() : 0);
            }
            action.m_event.Invoke();
            return delay;
        }


    }
}
