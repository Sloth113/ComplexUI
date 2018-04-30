using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    [SerializeField] private GameObject m_prefab;
    [SerializeField] private float m_spawnTime;
    private float m_timer = 0;
    [SerializeField] private int m_spawnLimit = 3;
    [SerializeField] private int m_spawnCount = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        m_timer += Time.deltaTime;
        if (m_timer >= m_spawnTime)
        {
            m_timer = 0;
            if (m_spawnCount < m_spawnLimit)
            {
                Spawn();
                m_spawnCount++;
            }
        }
	}

    public void Spawn()
    {
        Instantiate<GameObject>(m_prefab, transform.position, transform.rotation).GetComponent<ISpawnable>().SetSpanwer(this);

        //Effect?
    }

    public void ChangeSpawnCount(int amt)
    {
        m_spawnCount += amt;
    }
}
