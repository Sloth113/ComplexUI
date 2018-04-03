using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class MyButton :  Button {

    public float m_timer = 0;
    
    public Tween[] m_overTweens;
    public Tween[] m_exitTweens;
    public Tween[] m_downTweens;
    public Tween[] m_upTweens;
    public Tween[] m_enabledTweens;
    public Tween[] m_disabledTweens;

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        foreach(Tween t in m_overTweens)
        {
            if (t != null)
                t.Apply(this.gameObject);
        }
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        foreach (Tween t in m_exitTweens)
        {
            if (t != null)
                t.Apply(this.gameObject);
        }
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        foreach (Tween t in m_downTweens)
        {
            if (t != null)
                t.Apply(this.gameObject);
        }
    }
    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        foreach (Tween t in m_upTweens)
        {
            if (t != null)
                t.Apply(this.gameObject);
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
