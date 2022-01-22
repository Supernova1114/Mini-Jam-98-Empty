using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{

    private GameObject player;
    private Rigidbody playerRB;

    public float gravityFactor = -1;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerRB = player.GetComponent<Rigidbody>();
    }


    private void FixedUpdate()
    {
        Vector3 towardsPlanet = player.transform.position - transform.position;

        Vector3 xzPlane = new Vector3(towardsPlanet.x, 0, towardsPlanet.z);

        Vector3 gravityForce = (1 / xzPlane.magnitude) * gravityFactor * xzPlane.normalized * (transform.localScale.magnitude / 2.0f) * (player.transform.localScale.magnitude / 2.0f);

        playerRB.AddForce(gravityForce);

        print(gravityForce.magnitude);
    }
}
