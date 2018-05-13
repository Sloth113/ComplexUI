using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Clickable machine that takes food and outputs coins 
public class FoodMoney : MonoBehaviour, IClickable {
    [SerializeField] private Transform m_spawnPoint;
    [SerializeField] private GameObject m_prefab;
    [SerializeField] private int m_cost = 1; //coin per food
    [SerializeField] private float m_timeBetween = 0.5f;
    private float m_timer;
    private int m_spawnCount;
    private Text m_text;

    // Use this for initialization
    void Start () {
        m_text = GetComponentInChildren<Text>();
        m_text.text = "" + m_cost + " Food = " + 1 + " Coin";
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
    //Click location takes the cost and outputs coins 
    public void Click(Vector3 loc)
    {
        int count = GameManager.Instance.GetFood().amt / m_cost;
        m_spawnCount += count;
        GameManager.Instance.ChangeFood(- count * m_cost);
    }

}
