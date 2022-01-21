using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    private const float gravityConst = 0.0001f;
    public float radius;
    public float mass;
    public Vector3 initialVelo;
    public Vector3 currentVelo; 
    Rigidbody rb;

    void Awake () 
    {
        rb = GetComponent<Rigidbody>();
        currentVelo = initialVelo;
    }

    public void UpdateVelocity(Orbit[] allBodies, float timeStep) 
    {
        foreach (var otherBody in allBodies) 
        {
            if (otherBody != this) 
            {
                float sqrDst = (otherBody.rb.position - rb.position).sqrMagnitude;
                Vector3 forceDir = (otherBody.rb.position - rb.position).normalized;
                Vector3 force = forceDir * gravityConst * mass * otherBody.mass / sqrDst;
                Vector3 acceleration = force / mass;

                currentVelo += acceleration * timeStep;
            }
        }
    }
    public void UpdatePosition (float timeStep) 
    {
        rb.position += currentVelo * timeStep;
    }
}
