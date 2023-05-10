using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : CustomUpdater
{
    public int maxHealth = 100;
    public float angle;
    public float speed;
    public float movementDuration;
    public float cooldown;
    public float shootCooldown;
    public Transform spawnTransform;
    public GameObject bulletPrefab;
    public ObjectPool enemyPoolReference;
    private int currentHealth;
    private Vector3 currentDirection;
    private Quaternion targetRotation;
    private Rigidbody rb;
    private float currentShootCooldown;



    void Start()
    {
        UpdateManagerGameplay.Instance.Add(this);
        rb = GetComponent<Rigidbody>();
        currentHealth = maxHealth;
        GameObject enemyPool = GameObject.Find("ProyectilePool");
        enemyPoolReference = enemyPool.GetComponent<ObjectPool>();
        

    }

    public override void Tick()
    {
        Move();
        Shoot();
    }

    public void InitializeEnemy(Transform spawnEnemy)
    {
        transform.position = spawnEnemy.transform.position;
        transform.rotation = spawnEnemy.transform.rotation;
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>();
            Die();    
        }
    }

    private void Move()
    {
        cooldown -= Time.deltaTime;

        if (cooldown <= 0)
        {
            currentDirection = GetRandomDirection();
            cooldown = movementDuration;
            targetRotation = Quaternion.LookRotation(currentDirection);
        }

        rb.MovePosition(rb.position + currentDirection * speed * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.1f);
    }

    private Vector3 GetRandomDirection()
    {
        int randomInt = Random.Range(0, 4);
        switch (randomInt)
        {
            case 0:
                return Vector3.forward;
            case 1:
                return Vector3.right;
            case 2:
                return Vector3.back;
            case 3:
                return Vector3.left;
            default:
                return Vector3.zero;
        }
    }

    private void Shoot()
    {
        currentShootCooldown -= Time.deltaTime;
        if (currentShootCooldown <= 0)
        {
            GameObject bullet = enemyPoolReference.GetPooledObject();

            if (bullet != null)
            {
                bullet.transform.position = spawnTransform.transform.position;
                bullet.transform.rotation = spawnTransform.transform.rotation;
            }
            currentShootCooldown = shootCooldown;
        }
    }

    void Die()
    {
        ParticleSystem explosion = GetComponentInChildren<ParticleSystem>();
        if (explosion != null)
        {
            explosion.transform.parent = null;
            explosion.Play(); 
        }
        EnemyCounter.Instance.EnemyDie();
        enemyPoolReference.ReturnToPool(gameObject);
    }
}
