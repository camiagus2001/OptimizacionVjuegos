using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : CustomUpdater
{
    public int maxHealth = 100;
    [SerializeField] private int currentHealth;
    public float angle;
    public float speed;
    public float movementDuration;
    public float cooldown;
    public Transform spawnTransform;
    public GameObject bulletPrefab;
    private Vector3 currentDirection;
    private Quaternion targetRotation;
    private Rigidbody rb;
    
    void Start()
    {
        UpdateManagerGameplay.Instance.Add(this);
        rb = GetComponent<Rigidbody>();
        currentHealth = maxHealth;
        
    }

    public override void Tick()
    {
       Move();
       Shoot();
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
            Destroy(gameObject);
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
        cooldown -= Time.deltaTime;
        if (cooldown <= 0)
        {
            GameObject bullet = ObjectPool.instance.GetPooledObject();

            if (bullet != null)
            {
                bullet.transform.position = spawnTransform.transform.position;
                bullet.transform.rotation = spawnTransform.transform.rotation;
                bullet.SetActive(true);
            }
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
        UpdateManagerGameplay.Instance.Remove(this);
        Destroy(gameObject);
    }
}
