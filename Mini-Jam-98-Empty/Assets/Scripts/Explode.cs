using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    private GameObject player;
    public int cubesPerAxis = 3;
    public float explosionForce = 1000f;
    public float radius = 20f;
    private int time = 10;

    public int pointValueFactor = 1;
    private float playerX;
    private float playerY;
    private float playerZ;


    private void Start()
    {
        player = GameObject.Find("Player");
    }

    void FixedUpdate()
    {
        playerX = player.transform.position.x;
        playerY = player.transform.position.y;
        playerZ = player.transform.position.z;
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Player")
        {
            ExplodeCube();
            StartCoroutine(PlanetState());
            PointSystem.instance.AddPoints((int)Mathf.Ceil(transform.localScale.magnitude * pointValueFactor));
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

    private IEnumerator PlanetState()
    {
        float temp = gameObject.GetComponent<Rigidbody>().mass;

        gameObject.GetComponent<Rigidbody>().mass = 0f;
        gameObject.GetComponent<MeshRenderer>().enabled = false;

        yield return new WaitForSeconds(5);

        RepositionPlanet();
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        gameObject.GetComponent<Rigidbody>().mass = temp;
    }

    public void RepositionPlanet()
    { 
        gameObject.transform.position = GenerateNewVector(true);
        gameObject.transform.rotation = Quaternion.Euler(GenerateNewVector(false));
    }

    private Vector3 GenerateNewVector(bool isPos)
    {
        float x_min = 0;
        float x_max = 360;
        float y_min = 0;
        float y_max = 360;
        float z_min = 0;
        float z_max = 360;

        if (isPos)
        {
            x_min = playerX - GameboxConst.RespawnDistXZ;
            x_max = playerX + GameboxConst.RespawnDistXZ;

            y_min = playerY - GameboxConst.RespawnDistY;
            y_max = playerY + GameboxConst.RespawnDistY;

            z_min = playerZ - GameboxConst.RespawnDistXZ;
            z_max = playerZ + GameboxConst.RespawnDistXZ;
        }

        return new Vector3(Random.Range(x_min, x_max), 
                           Random.Range(y_min, y_max), 
                           Random.Range(z_min, z_max));
    }

}