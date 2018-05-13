using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Script used to spawn objects at a certain rate 
public class Spawner : MonoBehaviour {
    [SerializeField] private GameObject m_prefab;
    [SerializeField] private float m_spawnTime;
    private float m_timer = 0;
    [SerializeField] private int m_spawnLimit = 3;
    [SerializeField] private int m_spawnCount = 0;

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
    //Create thing
    public void Spawn()
    {
        Instantiate<GameObject>(m_prefab, transform.position, transform.rotation).GetComponent<ISpawnable>().SetSpanwer(this);

        //Effect?
    }
    //Changes the amount that can be spawned
    public void ChangeSpawnCount(int amt)
    {
        m_spawnCount += amt;
    }
}
