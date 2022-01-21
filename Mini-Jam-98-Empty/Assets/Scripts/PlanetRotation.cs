using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetRotation : MonoBehaviour
{
    public int spinSpeed = 1;

    // Probably switch to Start() later on
    void Update()
    {
        transform.Rotate(0, spinSpeed, 0);
    }
}
