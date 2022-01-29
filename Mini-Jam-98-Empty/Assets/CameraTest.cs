using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraTest : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera vcam1;
    [SerializeField]
    private CinemachineFreeLook vcam3;
    /*
    private CinemachinePOV firstPovComponent;
    private CinemachinePOV thirdPovComponent;
    */

    private bool flag = true;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        vcam1.Priority = 0;
        vcam3.Priority = 1;
        /*
        firstPovComponent = vcam1.GetCinemachineComponent<CinemachinePOV>();
        thirdPovComponent = vcam3.GetCinemachineComponent<CinemachinePOV>();
        */
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (flag)
            {
                flag = false;
                vcam1.Priority = 1;
                vcam3.Priority = 0;

                /*
                Vector3 euler = Quaternion.LookRotation(vcam3.transform.forward).eulerAngles;

                firstPovComponent.m_HorizontalAxis.Value = euler.y;
                firstPovComponent.m_VerticalAxis.Value = euler.x;
                */
            }
            else
            {
                flag = true;
                vcam1.Priority = 0;
                vcam3.Priority = 1;

                /*
                Vector3 euler = Quaternion.LookRotation(vcam1.transform.forward).eulerAngles;
                
                thirdPovComponent.m_HorizontalAxis.Value = euler.y;
                thirdPovComponent.m_VerticalAxis.Value = euler.x;
                */
            }
        }
    }
}
