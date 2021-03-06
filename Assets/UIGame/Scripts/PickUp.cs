﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//item pick up script
public class PickUp : MonoBehaviour, IClickable {
    [SerializeField] Items m_item;
    [SerializeField] int m_amount;
    [SerializeField] bool m_effectSpawn;


    public void Click(Vector3 loc)
    {
        ItemDetail pickUp;
        pickUp.amt = m_amount;
        pickUp.item = m_item;
        GameManager.Instance.AddItem(pickUp);
        if(m_effectSpawn)
        {
            Instantiate<GameObject>(Camera.main.GetComponent<GameManager>().GetEffect(m_item), loc, transform.rotation).GetComponent<CUI.WorldToUi>().enabled = true;
        }
        //Effect

        Destroy(gameObject);
    }

}
