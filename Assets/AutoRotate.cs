using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotate : MonoBehaviour {
    [SerializeField]
    Vector3 m_dir;
    [SerializeField]
    float m_speed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(m_dir * m_speed * Time.deltaTime);
	}
}
