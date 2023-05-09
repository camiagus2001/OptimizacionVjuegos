using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : CustomUpdater
{
    public float speed = 10f;
    public int damage = 10;
    public float lifetime = 2f;

    private float age;

    private void Start()
    {
        UpdateManagerGameplay.Instance.Add(this);
    }
   public override void Tick()
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
           // Destroy(gameObject); 
           gameObject.SetActive(false);
        }

        if (other.gameObject.CompareTag("Wall"))
        {
            Wall wall = other.gameObject.GetComponent<Wall>();
            wall.TakeDamage(damage);
            // Destroy(gameObject);
            gameObject.SetActive(false);
        }

        if (other.gameObject.CompareTag("Perimeter"))
        {
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }
}
