using UnityEngine;

public class TankMovement : MonoBehaviour //Agregar CustomUpdater 
{
    public int m_PlayerNumber = 1;
    public float Speed = 5f;                 
    public float  TurnSpeed = 180f;
              
    private string m_MovementAxisName;          
    private string m_TurnAxisName;              
    private Rigidbody m_Rigidbody;             
    private float m_MovementInputValue;        
    private float m_TurnInputValue;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }
    private void Start()
    {
      // UpdateManagerGameplay.Instance.Add(this);

        m_MovementAxisName = "Vertical" + m_PlayerNumber;
        m_TurnAxisName = "Horizontal" + m_PlayerNumber;
    }
    private void Update() //Tick
    {
        m_MovementInputValue = Input.GetAxis(m_MovementAxisName);
        m_TurnInputValue = Input.GetAxis(m_TurnAxisName);

        Move();
        Turn();
    }

    private void OnEnable()
    {      
        m_Rigidbody.isKinematic = false;
       
        m_MovementInputValue = 0f;
        m_TurnInputValue = 0f;      
    }

    private void OnDisable()
    {       
        m_Rigidbody.isKinematic = true;       
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            Destroy(gameObject);
        }
    }

    void Move()
    {      
        Vector3 movement = transform.forward * m_MovementInputValue * Speed * Time.deltaTime;
      
        m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
    }

     void Turn()
    {       
        float turn = m_TurnInputValue * TurnSpeed * Time.deltaTime;

        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        
        m_Rigidbody.MoveRotation(m_Rigidbody.rotation * turnRotation);
    }
   
}
