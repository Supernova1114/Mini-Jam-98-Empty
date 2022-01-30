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
    private float currentInterp;

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
        StartCoroutine("CenterOnStart");
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

                StartCoroutine("SetInterp");
            }
        }
        else
        {
            cooldown -= Time.deltaTime;
        }


        if (isRecentering)
        {
            Vector3 currRotation = recenterObj.transform.rotation.eulerAngles;

            //print(currRotation);

            //For some reason switching x and y values makes it work
            float newXAxis = Mathf.Lerp(xAxis, currRotation.y, currentInterp);
            float newYAxis = Mathf.Lerp(yAxis, currRotation.x, currentInterp);

            povComponent.m_HorizontalAxis.Value = newXAxis;
            povComponent.m_VerticalAxis.Value = newYAxis;

            //print("Recentering!");
        }

        /*Vector3 euler = Quaternion.LookRotation(mainCamera.transform.forward).eulerAngles;
        thirdPovComponent.m_HorizontalAxis.Value = euler.y;
        thirdPovComponent.m_VerticalAxis.Value = euler.x;*/


    }


    private IEnumerator SetInterp()
    {
        //print("SetInterp");
        currentInterp = recenterInterp;

        yield return new WaitForSeconds(1f);

        while (currentInterp < 0.99f)
        {
            //print(currentInterp);
            yield return new WaitForEndOfFrame();

            currentInterp = Mathf.Lerp(currentInterp, 1.0f, 0.05f);
        }

        //print("Success!"); 
        currentInterp = 1;
    }

    private IEnumerator CenterOnStart()
    {
        currentInterp = 1;
        isRecentering = true;
        yield return new WaitForSeconds(1);
        isRecentering = false;
        currentInterp = recenterInterp;

    }

}
