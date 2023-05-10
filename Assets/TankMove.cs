using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMove : MonoBehaviour
{
    public float moveSpeed = 5f;

    private void Update()
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

        transform.position += movement * moveSpeed * Time.deltaTime;

        if (movement != Vector3.zero) 
        {
            transform.rotation = Quaternion.LookRotation(movement);
        }
    }
}
