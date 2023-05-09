using UnityEngine;

public class Player : CustomUpdater
{
    public int m_PlayerNumber = 1;
    public float Speed = 5f;
    public float TurnSpeed = 180f;

    private string m_MovementAxisName;
    private string m_TurnAxisName;
    private Rigidbody m_Rigidbody;
    private float m_MovementInputValue;
    private float m_TurnInputValue;

    public int cantProyectiles;
    public int cantidadTotalProyectiles;

    public GameObject bulletPrefab;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        UpdateManagerGameplay.Instance.Add(this);

        m_MovementAxisName = "Vertical" + m_PlayerNumber;
        m_TurnAxisName = "Horizontal" + m_PlayerNumber;
    }
    public override void Tick()
    {
        m_MovementInputValue = Input.GetAxis(m_MovementAxisName);
        m_TurnInputValue = Input.GetAxis(m_TurnAxisName);


        if (Input.GetButtonDown("Fire1"))
        {
            ShootBullet();
        }

        Move();
        Turn();
        Reload();
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

    public void ShootBullet()
    {
        GameObject bullet = ObjectPool.instance.GetPooledObject();

        if (bullet != null && cantProyectiles > 0)
        {
            bullet.transform.position = transform.position;
            bullet.transform.rotation = transform.rotation;
            bullet.SetActive(true);
            cantProyectiles -= 1;
        }
    }

    public void Reload()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            cantProyectiles = cantidadTotalProyectiles;
        }
    }
}