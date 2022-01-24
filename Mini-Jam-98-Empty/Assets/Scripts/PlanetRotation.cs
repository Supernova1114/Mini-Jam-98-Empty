using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetRotation : MonoBehaviour
{
    public int x_speed = 0;
    public int y_speed = 0;
    public int z_speed = 0;

    // Probably switch to Start() later on
    void Update()
    {
        transform.Rotate(x_speed, y_speed, z_speed);
    }
}
