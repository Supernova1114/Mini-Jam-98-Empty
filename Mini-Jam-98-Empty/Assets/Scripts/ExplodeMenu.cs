using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeMenu : MonoBehaviour
{
    private GameObject chunk;
    public int cubesPerAxis = 3;
    public float explosionForce = 1000f;
    public float radius = 20f;
    private int time = 10;



    //This is bad workaround I made for Explod.cs and SpeedCap.cs mess, this and speedcap aer asdljasldjsalsaldj for menu scene

    void Start()
    {
        chunk = GameObject.Find("Chunk");
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Player")
        {
            ExplodeCube();
        }
    }

    public void ExplodeCube()
    {
        for (int x = 0; x < cubesPerAxis; x++)
        {
            for (int y = 0; y < cubesPerAxis; y++)
            {
                for (int z = 0; z < cubesPerAxis; z++)
                {
                    CreateDebris(new Vector3(x, y, z));
                }
            }
        }
    }

    public void CreateDebris(Vector3 croods)
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        Renderer rend = cube.GetComponent<Renderer>();
        rend.material = GetComponent<Renderer>().material;

        cube.transform.localScale = transform.localScale / cubesPerAxis;
        Vector3 firstCube = transform.position - transform.localScale / 2 + cube.transform.localScale / 2;
        cube.transform.position = firstCube + Vector3.Scale(croods, cube.transform.localScale);

        Rigidbody rb = cube.AddComponent<Rigidbody>();
        rb.useGravity = false;
        rb.mass = .5f;
        rb.AddExplosionForce(explosionForce, transform.position, radius, 3.0f, ForceMode.Force);

        Object.Destroy(cube, time);
    }


}