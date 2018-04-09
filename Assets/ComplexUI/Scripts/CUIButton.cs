using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace CUI
{
    //Extends Unity's button to be able to apply tweens on actions
    public class CUIButton : Button
    {
        [System.Serializable]
        public struct StateActions
        {
            public Tween[] m_tweens;
            public float m_delay;//Tweens get delay for each position in the list, first 0 delay, 2nd 1xdelay, 3rd 2xdelay.. 
            public bool m_squence; //Play tweens in a sequence., first 0 delay, 2nd 1xdelay+previous delay, 3rd 2xdelay + all previous delay. 
            public UnityEvent m_event; //fire off event when done
        }

        public StateActions m_onOver;
        public StateActions m_onExit;
        public StateActions m_onDown;
        public StateActions m_onUp;
        public StateActions m_onEnabled; //Make own enable/disable functions that do enable/disable after tweens? 
        public StateActions m_onDisabled;

        private bool m_enabled;

        //On Over
        public override void OnPointerEnter(PointerEventData eventData)
        {
            if (m_enabled)
            {
                base.OnPointerEnter(eventData);
                PlayAction(m_onOver);
            }   
        }

        //On Exit
        public override void OnPointerExit(PointerEventData eventData)
        {
            if (m_enabled)
            {
                base.OnPointerExit(eventData);
                PlayAction(m_onExit);
            }
        }

        //On down
        public override void OnPointerDown(PointerEventData eventData)
        {
            if (m_enabled)
            {
                base.OnPointerDown(eventData);
                PlayAction(m_onDown);
            }
        }
        //On up
        public override void OnPointerUp(PointerEventData eventData)
        {
            if (m_enabled)
            {
                base.OnPointerUp(eventData);
                PlayAction(m_onUp);
            }
        }
        //Enable
        public void Enable()
        {
            if (m_enabled == false)
            {
                m_enabled = true;
                gameObject.SetActive(true);
                PlayAction(m_onEnabled);
            }
        }
        
        //My disable function 
        public void Disable()
        {
            if (m_enabled)
            {
                m_enabled = false;
                //Use return value from play actions being called to start coroutine
                StartCoroutine(DisableAfter(PlayAction(m_onDisabled)));
            }
        }

        private IEnumerator DisableAfter(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            gameObject.SetActive(false);
        }

        private float PlayAction(StateActions action)
        {
            float delay = 0;
            foreach(Tween t in action.m_tweens)
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
