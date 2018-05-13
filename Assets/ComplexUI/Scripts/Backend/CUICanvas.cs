using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CUI
{
    //CUI canvas gets all child tween elements
    //Allows for global disable/enables of a canvas 
    //Has an apply all to children function that applies an action to every child
    public class CUICanvas : MonoBehaviour, ICUIElement
    {

        private bool m_enabled;
        //private Canvas m_canvas;

        [SerializeField] private StateActions m_action;

        //List<ICUIElement> m_childrenElements;
        Dictionary<ICUIElement, bool> m_childrenElements;// KEY = SCRIPT, VALUE = Pev enabled state
        // Use this for initialization
        void Start()
        {
            //m_canvas = GetComponent<Canvas>();
            m_enabled = enabled;

            
        }
        //Called once, before anything else (onEnable)
        private void Awake()
        {
            m_childrenElements = new Dictionary<ICUIElement, bool>();
            GetUI();
            //m_childrenElements = new List<ICUIElement>(GetComponentsInChildren<ICUIElement>(true));
            //m_childrenElements.Remove(this);
            //m_prevStates = new bool[m_childrenElements.Count];
            //GetUI();
        }
        //Add CUI elements to this excluding this
        private void GetUI()
        {
            ICUIElement[] elements = GetComponentsInChildren<ICUIElement>(true);
            foreach(ICUIElement el in elements)
            {
                if (el != this)
                {
                    m_childrenElements.Add(el, true);//True by default so that any canvas that is enabled for the first time shows everything
                    //Could have a flag in the element to say on by default
                }
            }
        }
        //Grabs current stats of UI so if it is already diabled they stay that way when the canvas is enabled
        private void CheckStates()
        {
            //Uses a list to remember all that needs to be changed as can not change dictionary while iterating
            List<ICUIElement> keyList = new List<ICUIElement>(); 
           foreach(KeyValuePair<ICUIElement, bool> element in m_childrenElements)
           {
                keyList.Add(element.Key);
            }
           foreach(ICUIElement key in keyList)
            {
                m_childrenElements[key] = (key.GetGameObject().activeInHierarchy);
            }
            
        }
        //Ensure the enabled variable is set
        private void OnDisable()
        {
            m_enabled = false;
        }
        private void OnEnable()
        {
            m_enabled = true;
        }
        //Plays an action on every child using CUI functions static class
        public void ApplyActionToChildren()
        {
            foreach(KeyValuePair<ICUIElement, bool> el in m_childrenElements)
            {
                if(el.Value)
                    CUIFunctions.PlayAction(m_action, el.Key.GetGameObject());
            }
        }

        //Disable, call disable on everything then disable self after everything is done
        public float Disable()
        {
            if (m_enabled)
            {

                CheckStates();
                m_enabled = false;
                //ICUIElement[] childrenElements = GetComponentsInChildren<ICUIElement>();
                float time = 0;
                foreach (KeyValuePair<ICUIElement, bool> element in m_childrenElements)
                {
                    float duration = element.Key.Disable();
                    if (time < duration)
                        time = duration;
                }
                StartCoroutine(CUIFunctions.DisableAfter(time, this.gameObject));
                return time;
            }
            return 0;
        }
        //Enable self then call enable on everything else
        public float Enable()
        {
            if (m_enabled == false)
            {

                m_enabled = true;
                gameObject.SetActive(true);//Calls awake and start here
               // ICUIElement[] childrenElements = GetComponentsInChildren<ICUIElement>();
                float time = 0;
                foreach (KeyValuePair<ICUIElement, bool> element in m_childrenElements)
                {
                    if (element.Value)
                        time += element.Key.Enable();
                }
                return time;
            }
            return 0;
        }
        //
        public GameObject GetGameObject()
        {
            return gameObject;
        }
    }
}
