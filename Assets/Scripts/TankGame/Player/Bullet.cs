using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : CustomUpdater
{
    public float speed = 10f;
    public int damage = 10;
    public float lifetime = 2f;
    private float age;
    private ObjectPool poolReference;

    private void Start()
    {
        UpdateManagerGameplay.Instance.Add(this);
        GameObject pojectilePool = GameObject.Find("ProyectilePool");
        poolReference = pojectilePool.GetComponent<ObjectPool>();
    }

    public void InitializeBullet(Transform spawnBullet)
    {
        transform.position = spawnBullet.transform.position;
        transform.rotation = spawnBullet.transform.rotation;
    }

    public override void Tick()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime); 
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(damage);
            poolReference.ReturnToPool(gameObject);
        }

        if (other.gameObject.CompareTag("Wall"))
        {
            Wall wall = other.gameObject.GetComponent<Wall>();
            wall.TakeDamage(damage);
            poolReference.ReturnToPool(gameObject);
        }

        if (other.gameObject.CompareTag("Perimeter"))
        {
            poolReference.ReturnToPool(gameObject);
        }

        if (other.gameObject.CompareTag("Player"))
        {
            Player player = other.gameObject.GetComponent<Player>();
            player.TakeDamage(damage);
            poolReference.ReturnToPool(gameObject);
        }
    }
}
