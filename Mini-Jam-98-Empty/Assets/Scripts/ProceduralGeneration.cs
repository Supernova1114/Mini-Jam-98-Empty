using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralGeneration : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private GameObject planet;

    //========================================================
    //========================================================

    public void Start() 
    {

    }
    void FixedUpdate()
    {
        transform.position = player.transform.position;
    }

    void OnTriggerExit(Collider other) 
    {
        if(other.gameObject.tag == "Planet")
        {
            planet = other.gameObject;

            planet.GetComponent<Explode>().RepositionPlanet();
        }
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