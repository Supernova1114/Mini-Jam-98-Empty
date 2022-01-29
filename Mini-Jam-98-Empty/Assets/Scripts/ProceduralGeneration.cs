//===================================================================================
// [ ✓ ] Fully functional & Stable
//===================================================================================

//======================================================================
// NEED TO:
//    - Spawn object at location that doesn't overlap with other objects
//    - Random Scale for obj & Different mass too
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralGeneration : MonoBehaviour
{
    public GameObject[] planetList;

    //========================================================
    //========================================================
    void Update()
    {
        foreach (GameObject a in planetList)
        {
            float dist = Vector3.Distance(a.transform.position, transform.position);

            if (dist > GameboxConst.RespawnDistX)
            {
                a.GetComponent<Explode>().RepositionPlanet();
            }
        }
    }
}