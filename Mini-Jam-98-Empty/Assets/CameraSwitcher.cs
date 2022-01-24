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

    [SerializeField]
    private float playerControlDelay;

    private bool isPlayerControl = true;

    private bool flag = true;

    // Start is called before the first frame update
    void Start()
    {
        DelayControl(playerControlDelay);
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (isPlayerControl)
        {
            if (Input.mousePosition != currentMousePosition || moveVector.magnitude > 0)
            {
                currentMousePosition = Input.mousePosition;

                cooldown = cooldownInit;

                flag = true;

                thirdPersonCamFixed.Priority = -1;
                thirdPersonCamPOV.Priority = 1;
            }
        }

        cooldown -= Time.deltaTime;

        if (cooldown <= 0 && flag)
        {
            flag = false;

            thirdPersonCamFixed.Priority = 1;
            thirdPersonCamPOV.Priority = -1;
        }


    }

    private void DelayControl(float time)
    {
        isPlayerControl = false;
        StartCoroutine("DelayPlayerControl", playerControlDelay);
    }

    private IEnumerator DelayPlayerControl(float time)
    {
        yield return new WaitForSeconds(time);
        isPlayerControl = true;
    }

}
