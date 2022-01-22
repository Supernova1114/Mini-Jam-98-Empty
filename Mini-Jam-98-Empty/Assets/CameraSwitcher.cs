using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    [SerializeField]
    private Cinemachine.CinemachineVirtualCamera thirdPersonCamPOV;
    [SerializeField]
    private Cinemachine.CinemachineVirtualCamera thirdPersonCamFixed;

    private Vector3 currentMousePosition = Vector3.zero;

    [SerializeField]
    private float cooldownInit = 1;
    private float cooldown = 0;

    private bool flag = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.mousePosition != currentMousePosition)
        {
            currentMousePosition = Input.mousePosition;

            cooldown = cooldownInit;

            flag = true;

            thirdPersonCamFixed.Priority = -1;
            thirdPersonCamPOV.Priority = 1;
        }
        else
        {
            cooldown -= Time.deltaTime;
        }

        if (cooldown <= 0 && flag)
        {
            flag = false;

            thirdPersonCamFixed.Priority = 1;
            thirdPersonCamPOV.Priority = -1;
        }

    }
}
