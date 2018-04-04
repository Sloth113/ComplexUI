using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class myEvent : UnityEvent<int> { };
public class CharController : MonoBehaviour {
    public delegate void VarChange(int amt);
    //public event VarChange HealthChange;
   // public static event VarChange HealthChange;
    public myEvent changeEvent;
    public myEvent attackChange;
   // public UnityEvent change2;
    [SerializeField] private int hp;
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
            List<CUI.Tween> l = CUI.TweensManager.Instance.GetTweens();
            foreach(CUI.Tween t in l)
            {
                t.Apply(this.gameObject);
            }
            
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            hp++;
            if (changeEvent != null)
                changeEvent.Invoke(1);
        }
	}
}
