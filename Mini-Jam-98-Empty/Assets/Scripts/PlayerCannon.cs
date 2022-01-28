using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCannon : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera thirdPersonVCam;
    [SerializeField]
    private CinemachineVirtualCamera firstPersonVCam;
    private CinemachinePOV firstPovComponent;
    private CinemachinePOV thirdPovComponent;

    [SerializeField]
    private FollowPlayerVelocity fpv;
    [SerializeField]
    private FollowTarget ft;


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
        firstPovComponent = firstPersonVCam.GetCinemachineComponent<CinemachinePOV>();
        thirdPovComponent = thirdPersonVCam.GetCinemachineComponent<CinemachinePOV>();


        Cursor.lockState = CursorLockMode.Locked; //May want to put this into another script idk

        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        playerRB = GetComponent<Rigidbody>();

        meshRenderer = GetComponent<MeshRenderer>();

        thirdPersonVCam.Priority = 1;
        firstPersonVCam.Priority = -1;

        


    }

    // Update is called once per frame
    void Update()
    {

        //print(thirdPovComponent.m_HorizontalAxis.Value);
        


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

                    playerRB.constraints = RigidbodyConstraints.FreezeAll;
                    playerRB.velocity = Vector3.zero;

                    cameraFollowersActive(false);

                    Vector3 euler = Quaternion.LookRotation(mainCamera.transform.forward).eulerAngles;
                    firstPovComponent.m_HorizontalAxis.Value = euler.y;
                    firstPovComponent.m_VerticalAxis.Value = euler.x;

                    //Move to first person cam
                    thirdPersonVCam.Priority = -1;
                    firstPersonVCam.Priority = 1;

                    
                    meshRenderer.enabled = false;

                }
                else
                {
                    inCannonState = false;

                    cooldown = cooldownInit;

                    rechargeFlag = true;

                    //print("FIRE!");

                    playerRB.constraints = RigidbodyConstraints.FreezeRotation;
                    playerRB.AddForce(mainCamera.transform.forward.normalized * cannonForce);

                    Vector3 euler = Quaternion.LookRotation(mainCamera.transform.forward).eulerAngles;
                    thirdPovComponent.m_HorizontalAxis.Value = euler.y;
                    thirdPovComponent.m_VerticalAxis.Value = euler.x;

                    //Move to third person cam
                    firstPersonVCam.Priority = -1;
                    thirdPersonVCam.Priority = 1;
                    cameraFollowersActive(true);



                    meshRenderer.enabled = true;


                }



            }
        }// if (cooldown <= 0)
        else
        {
            cooldown -= Time.deltaTime;
        }


    }

    private void cameraFollowersActive(bool value)
    {
        fpv.shouldFollow = value;
        ft.shouldFollow = value;
    }

}
