using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : MonoBehaviour, IClickable {
    [SerializeField] private GameObject m_spawnPrefab;
    [SerializeField] private float m_timeBetween = 1;
    private float m_timer = 0;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (m_timer < m_timeBetween)
            m_timer += Time.deltaTime;
	}

    public void Click(Vector3 loc)
    {
        if (m_timer >= m_timeBetween)
        {
            Instantiate<GameObject>(m_spawnPrefab, loc, transform.rotation);
        }       
    }
}
