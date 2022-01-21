using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCannon : MonoBehaviour
{
    [SerializeField]
    private float cooldownInit = 2;
    private float cooldown = 0;

    private Cinemachine.CinemachineFreeLook thirdPersonCam;
    private Cinemachine.CinemachineFreeLook firstPersonCam; 

    private bool inCannonState = false;

    private Rigidbody playerRB;

    [SerializeField]
    private float cannonForce = 1;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //May want to put this into another script idk

        thirdPersonCam = GameObject.FindGameObjectWithTag("3rdVCam").GetComponent<Cinemachine.CinemachineFreeLook>();
        firstPersonCam = GameObject.FindGameObjectWithTag("1stVCam").GetComponent<Cinemachine.CinemachineFreeLook>();

        playerRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        if (cooldown <= 0)
        {
            if (Input.GetButtonDown("Jump"))
            {
                if (!inCannonState)
                {
                    inCannonState = true;

                    playerRB.constraints = RigidbodyConstraints.FreezePosition;
                    playerRB.velocity = Vector3.zero;

                    //Move to first person cam
                    thirdPersonCam.Priority = -1;
                    firstPersonCam.Priority = 1;
                }
                else
                {
                    inCannonState = false;

                    cooldown = cooldownInit;

                    print("FIRE!");

                    playerRB.constraints = RigidbodyConstraints.None;
                    playerRB.AddForce(firstPersonCam.transform.forward.normalized * cannonForce);

                    thirdPersonCam.Priority = 1;
                    firstPersonCam.Priority = -1;

                }



            }
        }// if (cooldown <= 0)
        else
        {
            cooldown -= Time.deltaTime;
        }


    }
}
