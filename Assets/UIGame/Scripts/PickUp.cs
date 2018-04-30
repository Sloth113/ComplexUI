using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour, IClickable {
    [SerializeField] Items m_item;
    [SerializeField] int m_amount;


    public void Click(Vector3 loc)
    {
        ItemDetail pickUp;
        pickUp.amt = m_amount;
        pickUp.item = m_item;
        GameManager.Instance.AddItem(pickUp);
        //Effect

        Destroy(gameObject);
    }

}
