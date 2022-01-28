using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class RecenterPovVCam : MonoBehaviour
{
    [SerializeField]
    private string inputXName;
    [SerializeField]
    private string inputYName;

    //Will recenter to the object's forward vector
    [SerializeField]
    private GameObject recenterObj;

    [Range(0.0f, 1.0f)]
    [SerializeField]
    private float recenterInterp;

    private CinemachineVirtualCamera vCam;
    private CinemachinePOV povComponent;

    //Axis
    private float xAxis = 0;
    private float yAxis = 0;

    private float mouseMoveX;
    private float mouseMoveY;

    [SerializeField]
    private float cooldownInit;
    private float cooldown = 0;

    private bool isRecentering = false;
    private bool recenterFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        vCam = GetComponent<CinemachineVirtualCamera>();
        povComponent = vCam.GetCinemachineComponent<CinemachinePOV>();
    }

    // Update is called once per frame
    void Update()
    {

        float mouseMoveX = Input.GetAxisRaw(inputXName);
        float mouseMoveY = Input.GetAxisRaw(inputYName);

        if (Mathf.Abs(mouseMoveX) > 0 || Mathf.Abs(mouseMoveY) > 0)
        {
            //Then we know the axis has moved

            isRecentering = false;
            recenterFlag = true;


            //reset timer
            cooldown = cooldownInit;
        }

        xAxis = povComponent.m_HorizontalAxis.Value;
        yAxis = povComponent.m_VerticalAxis.Value;

        if (cooldown <= 0)
        { 
            //Recenter camera
            if (recenterFlag)
            {
                recenterFlag = false;
                isRecentering = true;
            }
        }
        else
        {
            cooldown -= Time.deltaTime;
        }


        if (isRecentering)
        {
            Vector3 targetForward = Quaternion.LookRotation(recenterObj.transform.forward).eulerAngles;

            float newXAxis = Mathf.Lerp(xAxis, targetForward.x, recenterInterp);
            float newYAxis = Mathf.Lerp(yAxis, targetForward.y, recenterInterp);

            povComponent.m_HorizontalAxis.Value = newXAxis;
            povComponent.m_VerticalAxis.Value = newYAxis;

            //print("Recentering!");
        }

        /*Vector3 euler = Quaternion.LookRotation(mainCamera.transform.forward).eulerAngles;
        thirdPovComponent.m_HorizontalAxis.Value = euler.y;
        thirdPovComponent.m_VerticalAxis.Value = euler.x;*/


    }
}
