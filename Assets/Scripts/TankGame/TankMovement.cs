using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMovement : MonoBehaviour
{
    public int m_PlayerNumber = 1;
    public float Speed = 12f;                 
    public float  TurnSpeed = 180f;
           
    public float m_PitchRange = 0.2f;

    private string m_MovementAxisName;          
    private string m_TurnAxisName;              
    private Rigidbody m_Rigidbody;             
    private float m_MovementInputValue;        
    private float m_TurnInputValue;           
       
    private ParticleSystem[] m_particleSystems;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {      
        m_Rigidbody.isKinematic = false;
       
        m_MovementInputValue = 0f;
        m_TurnInputValue = 0f;
     
        m_particleSystems = GetComponentsInChildren<ParticleSystem>();
        for (int i = 0; i < m_particleSystems.Length; ++i)
        {
            m_particleSystems[i].Play();
        }
    }

    private void OnDisable()
    {       
        m_Rigidbody.isKinematic = true;
     
        for (int i = 0; i < m_particleSystems.Length; ++i)
        {
            m_particleSystems[i].Stop();
        }
    }
    private void Start()
    {
        m_MovementAxisName = "Vertical" + m_PlayerNumber;
        m_TurnAxisName = "Horizontal" + m_PlayerNumber;     
    }

     void Update()
    {      
        m_MovementInputValue = Input.GetAxis(m_MovementAxisName);
        m_TurnInputValue = Input.GetAxis(m_TurnAxisName);
    }

    private void FixedUpdate()
    {     
        Move();
        Turn();
    }
   
    private void Move()
    {      
        Vector3 movement = transform.forward * m_MovementInputValue * Speed * Time.deltaTime;
      
        m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
    }

    private void Turn()
    {
       
        float turn = m_TurnInputValue * TurnSpeed * Time.deltaTime;

        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        
        m_Rigidbody.MoveRotation(m_Rigidbody.rotation * turnRotation);
    }
   
}
