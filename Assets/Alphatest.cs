using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alphatest : MonoBehaviour {
    CanvasGroup m_canvasGroup;
	// Use this for initialization
	void Start () {

        m_canvasGroup = this.GetComponent<CanvasGroup>();
        m_canvasGroup.alpha = 0;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
