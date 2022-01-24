using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBoxChooser : MonoBehaviour
{
    [SerializeField]
    List<Material> skyboxes;

    // Start is called before the first frame update
    void Start()
    {
        int randomNum = Random.Range(0, skyboxes.Count);
        RenderSettings.skybox = skyboxes[randomNum];
    }

}
