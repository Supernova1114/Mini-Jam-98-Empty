using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamRotation : MonoBehaviour
{
    public Vector2 turn;
    public float sensitivity = 1f;
    public int speed = 1;
    Rigidbody rb;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        CubeRotate();
        Move();

    }

    void CubeRotate()
    {
        turn.x += Input.GetAxis("Mouse X") * sensitivity;
        //turn.y += Input.GetAxis("Mouse Y") * sensitivity;
        transform.localRotation = Quaternion.Euler(0, turn.x, 0);
    }

    void Move()
    {
        //rb.AddForce(new Vector3(0, 0, speed * Time.deltaTime));
        rb.AddForce(transform.forward * speed, ForceMode.Force);
    }
}
