using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    [SerializeField] private int currentHealth;
    public int rutine;
    public float cooldown;
    public Quaternion rotation;
    public float angle;
    public float speed;

    void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        MoveRandomly();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void MoveRandomly()
    {
        cooldown += 1 * Time.deltaTime;
        if (cooldown >= 1) 
        {
            rutine = Random.Range(0, 2);
            cooldown = 0;
        }
        switch (rutine)
        {
            case 0:
                break;

            case 1:
                angle = Random.Range(0, 360);
                rotation = Quaternion.Euler(0, angle, 0);
                rutine++;
                break;

            case 2:
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 0.5f);
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
                break;
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
