using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MyButton :  Button {

    public float m_timer = 0;
    
    public ScriptableTest m_myUI;

    private bool m_isSelected = false;
    public bool IsSelected
    {
        get
        {
            return m_isSelected;
        }
        set
        {
            m_isSelected = value;
        }
    }
    
    public void Shake(float time)
    {
        if (iTween.Count(gameObject) == 0)
        {
            //iTween.ShakeRotation(this.gameObject, new Vector3(0, 0, 15), time);
            iTween.ShakeRotation(this.gameObject, iTween.Hash("name", "Shake", "amount", new Vector3(0, 0, 10), "time", time, "delay", 1.0f));
            iTween.MoveTo(gameObject, new Vector3(transform.position.x, transform.position.y + 30, transform.position.z), time);
            iTween.MoveFrom(gameObject, iTween.Hash("name", "Moveback", "amount", new Vector3(transform.position.x, transform.position.y, transform.position.z), "time", time, "delay", time));
        }
    }
}
