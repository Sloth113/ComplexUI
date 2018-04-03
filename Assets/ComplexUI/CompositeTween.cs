using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Applies multi tweens to an object on the apply call
[CreateAssetMenu(fileName = "Tween", menuName = "ComplexUI/Tween/Composite Tween", order = 0)]
public class CompositeTween : Tween
{
    [SerializeField] Tween[] m_tweens;

    public override void Apply(GameObject obj)
    {
     foreach(Tween t in m_tweens)
        {
            if(t != null)
                t.Apply(obj);
        }
    }
}
