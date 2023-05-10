using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : CustomUpdater
{
    public float speed = 5f;
    public float turnSpeed = 180f;
    public int maxHealth = 100;
    public int cantProyectiles;
    public int cantidadTotalProyectiles;
    public GameObject bulletPrefab;
    public Transform spawnBullet;
    public Transform respawnPoint;
    public ObjectPool projectilePoolReference;
    private int currentHealth;

    

    private void Awake()
    {
        Cursor.visible = false;
    }
    private void Start()
    {
        UpdateManagerGameplay.Instance.Add(this);
    }

    public override void Tick()
    {
        Move();
        Shoot();
        Reload();
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
        float horizontalInput = Input.GetAxisRaw("Horizontal1");
        float verticalInput = Input.GetAxisRaw("Vertical1");

        if (horizontalInput != 0 && verticalInput != 0)
        {
            if (Mathf.Abs(horizontalInput) > Mathf.Abs(verticalInput))
            {
                verticalInput = 0;
            }
            else
            {
                horizontalInput = 0;
            }
        }

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);
        movement = movement.normalized;

        transform.position += movement * speed * Time.deltaTime;

        if (movement != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(movement);
        }
    }

    public void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            GameObject bullet = projectilePoolReference.GetPooledObject();
            bullet.GetComponent<Bullet>().InitializeBullet(spawnBullet);
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

    public void Die()
    {
        transform.position = respawnPoint.position;
    }
}