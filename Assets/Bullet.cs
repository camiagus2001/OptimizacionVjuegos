using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 10;
    public float lifetime = 2f;

    private float age = 0f;

    void Start()
    {
        Destroy(gameObject, lifetime); 
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime); 
        age += Time.deltaTime; 
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(damage);
            Destroy(gameObject); 
        }
    }
}
