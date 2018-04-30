using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodMoney : MonoBehaviour, IClickable {
    [SerializeField] private Transform m_spawnPoint;
    [SerializeField] private GameObject m_prefab;
    [SerializeField] private int m_cost = 1; //coin per food
    [SerializeField] private float m_timeBetween = 0.5f;
    private float m_timer;
    private int m_spawnCount;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(m_spawnCount > 0 && m_timer > m_timeBetween)
        {
            m_spawnCount--;
            Instantiate<GameObject>(m_prefab, m_spawnPoint.position, m_spawnPoint.rotation);
            m_timer = 0;
        }
        if (m_timer < m_timeBetween)
            m_timer += Time.deltaTime;
    }
    public void Click(Vector3 loc)
    {

        int count = GameManager.Instance.GetFood().amt;
        m_spawnCount += count;
        GameManager.Instance.ChangeFood(-count);

    }

}
