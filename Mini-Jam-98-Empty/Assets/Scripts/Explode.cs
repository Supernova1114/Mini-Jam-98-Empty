using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    private int cubesPerAxis;
    private float delay = 1f;
    private float explosionForce = 100f;
    private float radius = 2f;
    
    void Start()
    {
        cubesPerAxis = Random.Range(3,6);
    }

    private void ExplodeCube()
    {
        
    }
}
