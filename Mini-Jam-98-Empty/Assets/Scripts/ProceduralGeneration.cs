using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralGeneration : MonoBehaviour
{
    private const int chunkHalfSizeXZ = 375;
    private const int chunkHalfSizeY = 125;
    private const float minScale = 0.1f;
    private const float maxScale = 10.0f;

    [SerializeField] private GameObject player;
    private Vector3 planetPos;
    private Quaternion planetRot;

    public GameObject[] planets;
    bool planetExitChunk;

    //========================================================
    //========================================================

    void Start()
    {
        planetExitChunk = false;
        SpawnPlanet();
    }

    void FixedUpdate()
    {
        transform.position = player.transform.position;
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Planet")
            planetExitChunk = true;
    }

    //========================================================
    //========================================================

    void SpawnPlanet()
    {
        foreach(GameObject a in planets)
        {
            //===   Init planet's pos & rotation   ======
            InitPlanetSpawn(a);

            //===   Instantiate obj   ====================
            Instantiate(a, planetPos, planetRot);

            //===   Init planet's properties   ===========
            a.gameObject.tag = "Planet";
            a.AddComponent<BoxCollider>();
        }
    }



    void RespawnPlanet()
    {
        foreach(GameObject a in planets)
        {
            if(planetExitChunk)
            {
                InitPlanetSpawn(a);
            }
        }
    }
    


    //========================================================
    // Init Planet spawn prop/pos/rot
    //========================================================
    private void InitPlanetSpawn(GameObject a)
    {
        planetPos = GenerateRandomVector(true, chunkHalfSizeXZ, chunkHalfSizeY);
        planetRot = Quaternion.Euler(GenerateRandomVector(false, 0,0));

        //a.transform.localScale *= Random.Range(minScale,minScale);
    }



    //========================================================
    // Return random vector for pos/rot
    //========================================================
    private Vector3 GenerateRandomVector(bool isPos, int wl, int h)
    {
        float xMin, xMax;
        float yMin, yMax;
        float zMin, zMax;

        if(isPos)
        {
            xMin = player.transform.position.x - wl;
            xMax = player.transform.position.x + wl;

            yMin = player.transform.position.y - h;
            yMax = player.transform.position.y + h;

            zMin = player.transform.position.z - wl;
            zMax = player.transform.position.z + wl;
        }
        else
        {
            xMin = 0; yMin = 0; zMin = 0;
            xMax = 360; yMax = 360; zMax = 360;
        }

        return new Vector3(Random.Range(xMin, xMax), 
                           Random.Range(yMin, yMax), 
                           Random.Range(zMin, zMax));
    }
}

//======================================================================
// NEED TO:
//    - Spawn object at location that doesn't overlap with other objects
//    - OnTriggerExit
//    - Random Scale for obj
//    - Make it so spawned obj are indipendent from parent obj (chunk)
//    - Different mass too
//    - Add Find tag to apply Gravitational Pull for spawned obj
//======================================================================