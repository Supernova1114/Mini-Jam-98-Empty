using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCannon : MonoBehaviour
{
    [SerializeField]
    private Cinemachine.CinemachineVirtualCamera thirdPersonVCam;
    [SerializeField]
    private Cinemachine.CinemachineVirtualCamera firstPersonVCam;

    [SerializeField]
    private float cooldownInit = 2;
    private float cooldown = 0;

    private Camera mainCamera;

    private bool inCannonState = false;

    private Rigidbody playerRB;

    [SerializeField]
    private float cannonForce = 1;

    private MeshRenderer meshRenderer;

    [SerializeField]
    private ParticleSystem rechargeParticles;

    private bool rechargeFlag = false;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //May want to put this into another script idk

        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        playerRB = GetComponent<Rigidbody>();

        meshRenderer = GetComponent<MeshRenderer>();

    }

    // Update is called once per frame
    void Update()
    {

        if (cooldown <= 0)
        {
            if (rechargeFlag)
            {
                rechargeFlag = false;
                rechargeParticles.Play();
            }

            if (Input.GetButtonDown("Jump"))
            {
                if (!inCannonState)
                {
                    inCannonState = true;

                    playerRB.constraints = RigidbodyConstraints.FreezePosition;
                    playerRB.velocity = Vector3.zero;

                    //Move to first person cam
                    thirdPersonVCam.Priority = -1;
                    firstPersonVCam.Priority = 1;

                    meshRenderer.enabled = false;

                    print("ASDLAS");
                }
                else
                {
                    inCannonState = false;

                    cooldown = cooldownInit;

                    rechargeFlag = true;

                    //print("FIRE!");

                    playerRB.constraints = RigidbodyConstraints.None;
                    playerRB.AddForce(mainCamera.transform.forward.normalized * cannonForce);

                    //Move to third person cam
                    thirdPersonVCam.Priority = 1;
                    firstPersonVCam.Priority = -1;

                    meshRenderer.enabled = true;

                    print("HE");

                }



            }
        }// if (cooldown <= 0)
        else
        {
            cooldown -= Time.deltaTime;
        }


    }
}
