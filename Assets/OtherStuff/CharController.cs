using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
//Seting custom event systems, used to test events
[System.Serializable]
public class myEvent : UnityEvent<int> { };
public class CharController : MonoBehaviour {
    public delegate void VarChange(int amt);
    //public event VarChange HealthChange;
   // public static event VarChange HealthChange;
    public myEvent changeEvent;
    UnityEngine.Events.UnityEvent anotherEvent;
    public myEvent attackChange;
   // public UnityEvent change2;
    [SerializeField] private int hp;
    public CUI.CUIButton button;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            hp--;
            if (changeEvent != null)
                // HealthChange(-1);
                changeEvent.Invoke(-1);
            /* //Tween loader
            List<CUI.Tween> l = CUI.TweensManager.Instance.GetTweens();
            foreach(CUI.Tween t in l)
            {
                t.Apply(this.gameObject);
            }
            */
            button.Disable();
            
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            hp++;
            if (changeEvent != null)
                changeEvent.Invoke(1);
            button.Enable();
        }
	}
}
