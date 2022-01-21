using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simulation : MonoBehaviour
{
    private const float timeStep = 0.01f;
    public Orbit[] bodies;

    void Awake()
    {
        bodies = FindObjectsOfType<Orbit>();
        Time.fixedDeltaTime = timeStep;
    }

    void FixedUpdate()
    {
        for (int i = 0; i < bodies.Length; i++)
            bodies[i].UpdateVelocity(bodies, timeStep);

        for (int i = 0; i < bodies.Length; i++)
            bodies[i].UpdatePosition(timeStep);
    }
}
