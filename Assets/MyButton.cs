using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyButton :  Button {

    public float m_timer = 0;

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
}
