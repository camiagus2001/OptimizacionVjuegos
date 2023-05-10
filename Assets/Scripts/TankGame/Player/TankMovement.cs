using UnityEngine;

public class TankMovement : CustomUpdater 
{   
    public float Speed = 5f;                 
   
    private Rigidbody m_Rigidbody;

    public int cantProyectiles;
    public int cantidadTotalProyectiles;

    public GameObject bulletPrefab;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }
    private void Start()
    {
       UpdateManagerGameplay.Instance.Add(this);

    }
    public override void Tick() 
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

        transform.position += movement * Speed * Time.deltaTime;

        if (movement != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(movement);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            ShootBullet();
        }
        
        Reload();
    }

    private void OnEnable()
    {      
        m_Rigidbody.isKinematic = false;         
    }

    private void OnDisable()
    {       
        m_Rigidbody.isKinematic = true;       
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Wall")) 
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero; 
        }
    }
 
    public void ShootBullet()
    {       
        
        GameObject bullet = ObjectPool.instance.GetPooledObject();
            
        if(bullet != null && cantProyectiles > 0 )
        {
            bullet.transform.position = transform.position;
            bullet.transform.rotation = transform.rotation;
            bullet.SetActive(true);
            cantProyectiles -= 1;
        }
    }

    public void Reload()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            cantProyectiles = cantidadTotalProyectiles;         
        }
    }

}
