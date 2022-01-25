using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraTest : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera vcam1;
    [SerializeField]
    private CinemachineVirtualCamera vcam3;

    private bool flag = true;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        vcam1.Priority = 0;
        vcam3.Priority = 1;
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
            }
            else
            {
                flag = true;
                vcam1.Priority = 0;
                vcam3.Priority = 1;
            }
        }
    }
}
