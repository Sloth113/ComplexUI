using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Controls for the camera movement
//WASD for movement, SPACE/Control 
//Q E to rotate 
public class SimpleControls : MonoBehaviour {
    [SerializeField] float m_speed = 10f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 move = transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal") + transform.up * Input.GetAxis("Jump");
        transform.position += move * m_speed * Time.deltaTime;
        transform.Rotate(Vector3.up, Input.GetAxis("Look") * m_speed * 3 * Time.deltaTime);
    }
}
