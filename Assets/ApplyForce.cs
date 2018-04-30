using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyForce : MonoBehaviour
{
    [SerializeField] private Vector3 m_direction;
    [SerializeField] private float m_forceMulti = 1;

    [SerializeField] private int m_randVar;

    private Rigidbody m_rBody;
    

    private void Awake()
    {
        m_rBody = GetComponent<Rigidbody>();
        Apply();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Apply()
    {
        if (m_rBody != null)
        {
            m_rBody.AddForce(m_direction * m_forceMulti);
        }
    }

    public void Apply(Vector3 m_dir)
    {
        if (m_rBody != null)
        {
            m_rBody.AddForce(m_dir * m_forceMulti);
        }
    }
}
