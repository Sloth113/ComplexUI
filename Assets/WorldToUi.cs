using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CUI
{
    public class WorldToUi : MonoBehaviour
    {
        public Transform m_target;
        public GameObject m_UIElement;
        private Vector3 m_startPos;
        private Vector3 m_targetPos;
        
        [SerializeField] private float m_lerpTime = 2;
        [SerializeField]private float m_timer = 0;
        [SerializeField] private UnityEvent m_onHit;
        // Use this for initialization
        void Start()
        {
            m_startPos = transform.position;
            //Calculate target
            RectTransform canvRect = m_UIElement.GetComponent<RectTransform>();

            RectTransformUtility.ScreenPointToWorldPointInRectangle(canvRect, new Vector2(10,10), Camera.main, out m_targetPos);
            Debug.Log(m_targetPos);

        }

        // Update is called once per frame
        void Update()
        {
            transform.position = Vector3.Lerp(m_startPos, m_target.position, m_timer / m_lerpTime);
            m_timer += Time.deltaTime;
            if (transform.position == m_target.position)
                Completed();
        }

        private void Completed()
        {
            m_onHit.Invoke();
            Destroy(gameObject);
        }
        
    }
}