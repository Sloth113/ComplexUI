using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CUI
{
    
    public class CUICanvas : MonoBehaviour, ICUIElement
    {

        private bool m_enabled;
        private Canvas m_canvas;

        [SerializeField] private StateActions m_action;

        //List<ICUIElement> m_childrenElements;
        Dictionary<ICUIElement, bool> m_childrenElements;// KEY = SCRIPT, VALUE = Pev enabled state
        // Use this for initialization
        void Start()
        {
            m_canvas = GetComponent<Canvas>();
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
        private void OnDisable()
        {
            m_enabled = false;
        }
        private void OnEnable()
        {
            m_enabled = true;
        }

        public void ApplyActionToChildren()
        {
            foreach(KeyValuePair<ICUIElement, bool> el in m_childrenElements)
            {
                if(el.Value)
                    CUIFunctions.PlayAction(m_action, el.Key.GetGameObject());
            }
        }

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

        public GameObject GetGameObject()
        {
            return gameObject;
        }
    }
}
