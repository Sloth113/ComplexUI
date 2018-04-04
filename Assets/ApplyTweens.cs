using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Currently test class to apply tween per timer
public class ApplyTweens : MonoBehaviour {
    [SerializeField] CUI.Tween[] m_tweens;
    [SerializeField] float m_time;
    private float m_timer;
    private int m_index;
	// Use this for initialization
	void Start () {
        m_timer = 0;
        m_index = 0;

    }
	
	// Update is called once per frame
	void Update () {
        m_timer += Time.deltaTime;

        if(m_timer > m_time)
        {
            if (m_index < m_tweens.Length)
            {
                if (m_tweens[m_index] != null)
                {
                   
                    m_tweens[m_index].Apply(this.gameObject);

                    m_index++;
                    if (m_index >= m_tweens.Length)
                    {
                        m_index = 0;
                    }
                    
                }
            }
            
            m_timer = 0;
        }
	}
}
