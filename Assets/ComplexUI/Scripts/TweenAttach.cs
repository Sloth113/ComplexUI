using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
//Script to attach tweens to objects that can be played using variables or calling the Play Var
namespace CUI
{
    public class TweenAttach : MonoBehaviour
    {
        public Tween m_tween;

        public bool m_onStart;
        public bool m_destroyOnPlay;
        public bool m_disableOnPlay;
        [Space]
        public bool m_inUpdate;
        public float m_playAfterTime;
        public bool m_playOnce;
        public UnityEvent m_event;
        private float m_timer;
        // Use this for initialization
        void Start()
        {
            if (m_onStart)
            {
                PlayTween();
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (m_inUpdate)
            {
                if (m_timer >= m_playAfterTime)
                {
                    PlayTween();
                    if(m_playOnce)
                        m_inUpdate = false;
                    m_timer = 0;
                }
                else
                {
                    m_timer += Time.deltaTime;
                }
                
            }
        }

        public void PlayTween()
        {
            if (m_tween != null)
            {
                m_tween.Apply(this.gameObject);
                m_event.Invoke();
                if (m_destroyOnPlay)
                    Destroy(this);
                if (m_disableOnPlay)
                    Disable();
            }
        }

        public void Enable()
        {
            this.enabled = true;
        }
        public void Disable()
        {
            this.enabled = false;
        }
        //Also if they can to just apply one now
        public static void PlayTween(GameObject obj, Tween tween)
        {
            tween.Apply(obj);
        }
    }
}
