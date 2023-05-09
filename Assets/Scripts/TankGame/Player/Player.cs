using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : CustomUpdater
{
    public int m_PlayerNumber = 1;
    public float Speed = 5f;
    public float TurnSpeed = 180f;

    public int maxHealth = 100;
    [SerializeField] private int currentHealth;

    private string m_MovementAxisName;
    private string m_TurnAxisName;
    private Rigidbody m_Rigidbody;
    private float m_MovementInputValue;
    private float m_TurnInputValue;

    public int cantProyectiles;
    public int cantidadTotalProyectiles;

    public GameObject bulletPrefab;
    public Transform spawnBullet;

    public Transform respawnPoint;

    

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        Cursor.visible = false;
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
            collision.gameObject.GetComponent<Enemy>();
            Die();
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
            bullet.transform.position = spawnBullet.transform.position;
            bullet.transform.rotation = spawnBullet.transform.rotation;
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

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        transform.position = respawnPoint.position;
    }
}