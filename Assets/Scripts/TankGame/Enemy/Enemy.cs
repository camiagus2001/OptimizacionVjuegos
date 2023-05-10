using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    [SerializeField] private int currentHealth;
   
    public float cooldown;
    public float angle;
    public float speed;

    public float moveSpeed = 5f;
    public float movementDuration = 2f;

    private Rigidbody rb;
    private Vector3 currentDirection;
    private float remainingMovementTime;
    private Quaternion targetRotation;

    void Start()
    {
        currentHealth = maxHealth;

        rb = GetComponent<Rigidbody>();
        currentDirection = GetRandomDirection();
        remainingMovementTime = movementDuration;
        targetRotation = transform.rotation;
    }

    private void Update()
    {      
        Cursor.visible = false;
    }

   
    private void FixedUpdate()
    {
        remainingMovementTime -= Time.fixedDeltaTime;

        if (remainingMovementTime <= 0)
        {
            currentDirection = GetRandomDirection();
            remainingMovementTime = movementDuration;
            targetRotation = Quaternion.LookRotation(currentDirection);
        }

        rb.MovePosition(rb.position + currentDirection * moveSpeed * Time.fixedDeltaTime);
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
            TankMovement Player = collision.gameObject.GetComponent<TankMovement>();
            Die();
            Destroy(gameObject);
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
        Destroy(gameObject);
    }
}
