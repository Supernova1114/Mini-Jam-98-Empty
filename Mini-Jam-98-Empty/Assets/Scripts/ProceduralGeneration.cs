//===================================================================================
// NEEDS REVAMP:
//===================================================================================
//    - Approach #1: Destory & Istantiate objs when leaving chunk
//          Draw back: Needs to add all components carefully or there
//                          will be too many of one components
//                     Add to Gravity list<> (easier) || GameObj[] array (dont do this)
//
//    - Approach #2: Respawn current obj in chunk to a random different location
//                      possibly different size & material (rend) too
//          Draw back: Unreliable (this is our current approach) and probably won't
//                          work correctly, unless my code is actually spaghetti
//
//    - Approach #3: Screw the chunk method, and use vectors/direction spawning.
//                      Calc dist between every obj (either in a pre-existing list)
//                      or istantiated, despawn it and spawn new one base on the 
//                      the direction the player is looking at
//          Draw back (on paper): Too much work for the computer since its checking
//                          & for a dozen different stuff every update calls
//                          Also, how do you even make something like this bruh
//===================================================================================
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
// NEED TO (OLD):
//    - Spawn object at location that doesn't overlap with other objects
//    - OnTriggerExit
//    - Random Scale for obj
//    - Make it so spawned obj are indipendent from parent obj (chunk)
//    - Different mass too
//    - Add Find tag to apply Gravitational Pull for spawned obj
//======================================================================