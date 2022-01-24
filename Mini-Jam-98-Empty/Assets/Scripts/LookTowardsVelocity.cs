using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookTowardsVelocity : MonoBehaviour
{
    private Rigidbody playerRB;
    
    private Cinemachine.CinemachineVirtualCamera vCam;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        vCam = GetComponent<Cinemachine.CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector3 velocity = playerRB.velocity;

        if (velocity != Vector3.zero)
            vCam.transform.forward = velocity;
    }
}
