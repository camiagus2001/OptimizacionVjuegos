using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    [SerializeField] private int currentHealth;
    private int rutine;
    public float cooldown;
    private Quaternion rotation;
    public float angle;
    public float speed;

    public Transform spawnTransform;
    public GameObject bulletPrefab;

    void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        MoveRandomly();
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
            Player Player = collision.gameObject.GetComponent<Player>();
            Die();
            Destroy(gameObject);
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

    private void Shoot()
    {
        cooldown += 1 * Time.deltaTime;
        if (cooldown >= 1)
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
        Destroy(gameObject);
    }
}
