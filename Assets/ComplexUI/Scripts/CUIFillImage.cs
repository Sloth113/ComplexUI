using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Used for images that are used as fill images
namespace CUI
{
    [RequireComponent(typeof(Image))]
    public class CUIFillImage : MonoBehaviour, ICUIElement
    {
        private Image m_image;
        private float m_amount;
        private float m_maxValue = 1;
        private float m_minValue = 0;


        public StateActions m_onEnabled; 
        public StateActions m_onDisabled;


        // Use this for initialization
        void Start()
        {
            m_image = GetComponent<Image>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public float Disable()
        {
            throw new System.NotImplementedException();
        }

        public float Enable()
        {
            throw new System.NotImplementedException();
        }

    }

}