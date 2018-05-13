using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Interactable object that spanws items on click from where it was clicked
public class Farm : MonoBehaviour, IClickable {
    [SerializeField] private GameObject m_spawnPrefab;
    [SerializeField] private float m_timeBetween = 1;
    private float m_timer = 0;

    	
	// Update is called once per frame
	void Update () {
        if (m_timer < m_timeBetween)
            m_timer += Time.deltaTime;
	}
    //Click position if it can spawn it will 
    public void Click(Vector3 loc)
    {
        if (m_timer >= m_timeBetween)
        {
            Instantiate<GameObject>(m_spawnPrefab, loc, transform.rotation);
            m_timer = 0;
        }       
    }
}
