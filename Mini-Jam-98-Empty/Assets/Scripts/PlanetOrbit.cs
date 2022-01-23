using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetOrbit : MonoBehaviour
{
    private const float G = 10.0f;
    public GameObject[] planets;

    //========================================================
    //========================================================

    void Start()
    {
        //planets = GameObject.FindGameObjectsWithTag("Planet");
        ApplyGravitationalRule(true);
    }

    void FixedUpdate()
    {
        ApplyGravitationalRule(false);
    }

    //========================================================
    //========================================================

    void ApplyGravitationalRule(bool isOrbital)
    {
        foreach(GameObject a in planets)
        {
            foreach(GameObject b in planets)
            {
                Vector3 a_pos = a.transform.position;
                Vector3 b_pos = b.transform.position;

                if(!a.Equals(b))
                {
                    Rigidbody a_rb = a.GetComponent<Rigidbody>();
                    Rigidbody b_rb = b.GetComponent<Rigidbody>();

                    float r = Vector3.Distance(a_pos, b_pos);

                    NewtonLaw(a_rb, b_rb, a_pos, b_pos, r);

                    if(isOrbital)
                    {
                        CircularOrbitalSpeed(a, b, a_rb, b_rb, r);
                    }
                }
            }
        }
    }



    //========================================================
    // Using Newton's law of universal gravitation:
    //      F = G * (m1 * m2) / r^2      
    //========================================================
    void NewtonLaw(Rigidbody a_rb, Rigidbody b_rb, 
                   Vector3 a_pos, Vector3 b_pos, float r)
    {
        a_rb.AddForce((b_pos - a_pos).normalized *  //   Add Force towards planet:
            (G * (a_rb.mass * b_rb.mass)            //         G × (m1 × m2) 
                    / (r * r)));                    //        ̅ ̅ ̅ ̅ ̅ ̅ ̅r̅²̅ ̅ ̅ ̅ ̅ ̅ ̅ 
    }



    //========================================================
    // Using Circular Orbital speed equation:
    //      v = sqrt((G * M) / r)
    //========================================================
    void CircularOrbitalSpeed(GameObject a, GameObject b,
                              Rigidbody a_rb, Rigidbody b_rb,float r)
    {
        a.transform.LookAt(b.transform);

        // Make obj go wee woo wee woo you spin me right round baby right round
        //     v       =                           sqrt((G *     M    ) / r)
        a_rb.velocity += a.transform.right * Mathf.Sqrt((G * b_rb.mass) / r);
    }
}