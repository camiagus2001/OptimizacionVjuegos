using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    void Start()
    {
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

    void Die()
    {
        ParticleSystem explosion = GetComponentInChildren<ParticleSystem>();
        if (explosion != null)
        {
            explosion.transform.parent = null; // Detach the particle system from the enemy object
            explosion.Play(); // Play the particle system
        }

        Destroy(gameObject);
    }
}
