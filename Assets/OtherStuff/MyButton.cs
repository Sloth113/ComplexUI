using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//First iteraction of a custom button, class mostly used in testing 
//Functionallity moved to CUI button class
namespace CUI
{

    public class MyButton : Button
    {

        public float m_timer = 0;
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

        /*
        public Tween[] m_overTweens;
        public Tween[] m_exitTweens;
        public Tween[] m_downTweens;
        public Tween[] m_upTweens;
        public Tween[] m_enabledTweens;
        public Tween[] m_disabledTweens;
        //*/

        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);
            float delay = 0;
            foreach (Tween t in m_onOver.m_tweens)
            {
                if (t != null)
                    t.Apply(this.gameObject, delay);
                delay += m_onOver.m_delay + (m_onOver.m_squence ? t.GetDuration() : 0);
            }
            m_onOver.m_event.Invoke();
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);

            float delay = 0;
            foreach (Tween t in m_onExit.m_tweens)
            {
                if (t != null)
                    t.Apply(this.gameObject, delay);
                delay += m_onExit.m_delay + (m_onExit.m_squence ? t.GetDuration() : 0);
            }
            m_onExit.m_event.Invoke();
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            float delay = 0;
            foreach (Tween t in m_onDown.m_tweens)
            {
                if (t != null)
                    t.Apply(this.gameObject, delay);
                delay += m_onDown.m_delay + (m_onDown.m_squence ? t.GetDuration() : 0);
            }
            m_onDown.m_event.Invoke();
        }
        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            float delay = 0;
            foreach (Tween t in m_onUp.m_tweens)
            {
                if (t != null)
                    t.Apply(this.gameObject, delay);
                delay += m_onUp.m_delay + (m_onUp.m_squence ? t.GetDuration() : 0);
            }
            m_onUp.m_event.Invoke();
        }

        public void Shake(float time)
        {
            if (iTween.Count(gameObject) == 0)
            {
                //iTween.ShakeRotation(this.gameObject, new Vector3(0, 0, 15), time);
                iTween.ShakeRotation(this.gameObject, iTween.Hash("name", "Shake", "amount", new Vector3(0, 0, 10), "time", time, "delay", 1.0f));
                iTween.MoveTo(gameObject, new Vector3(transform.position.x, transform.position.y + 30, transform.position.z), time);
                iTween.MoveFrom(gameObject, iTween.Hash("name", "Moveback", "amount", new Vector3(transform.position.x, transform.position.y, transform.position.z), "time", time, "delay", time));
            }
        }
    }
}
