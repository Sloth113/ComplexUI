using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TestEventLisntener : MonoBehaviour {

    private Text m_textAmt;
    private int m_hp;
    
    
	// Use this for initialization
	void Start () {
        m_textAmt = GetComponent<Text>();
        m_hp = 10;//GET CONTROLLER AND SET
        m_textAmt.text = ""+m_hp;
	}
    private void OnEnable()
    {
       // CharController.HealthChange += CharController_HealthChange;
    }

    public void CharController_HealthChange(int amt)
    {
        m_hp += amt;
        m_textAmt.text = "" + m_hp;

    }

    private void OnDisable()
    {
       // CharController.HealthChange -= CharController_HealthChange;
    }
    // Update is called once per frame
    void Update () {
		
	}
}
