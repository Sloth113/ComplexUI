using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public struct Drop
{
    public GameObject prefab;
    public float amount; //chance drops
}
//Used for wandering to random locations within a range
[RequireComponent(typeof(CharacterController))]
public class Wanderer : MonoBehaviour, IClickable, ISpawnable
{
    private NavMeshAgent m_navAgent;
    private CharacterController m_controller;
    public Spawner m_spawner;
    [SerializeField] private Vector3 m_dest;

    [SerializeField] List<Drop> m_drops;

    //Get components
    private void Awake()
    {
        
        m_controller = GetComponent<CharacterController>();
        m_navAgent = GetComponent<NavMeshAgent>();
        m_dest = new Vector3(Random.Range(-4.0f, 4.0f), 1.25f, Random.Range(-4.0f, 4.0f));
        m_navAgent.destination = m_dest;
    }
    //Set location if at target
	void Update () {
        if((transform.position - m_dest).sqrMagnitude < 0.5)
        {
            m_dest = new Vector3(Random.Range(-4.0f, 4.0f), 1.25f, Random.Range(-4.0f, 4.0f));
            m_navAgent.destination = m_dest;
        }
	}
    //If clicked spawn a thing
    //Also change spawn count
    public void Click(Vector3 loc)
    {
        if(m_spawner != null)
        {
            m_spawner.ChangeSpawnCount(-1);
        }
        foreach (Drop d in m_drops)
        {
            float amt = d.amount;
            while (amt > 0)
            {

                float rnd = Random.Range(0.0f, 1.0f);

                if (amt > rnd)
                {
                    Instantiate<GameObject>(d.prefab, transform.position, transform.rotation);
                }
                amt--;
            }
        }
        Destroy(gameObject);
    }


    public void SetSpanwer(Spawner spawner)
    {
        m_spawner = spawner;
    }
}
