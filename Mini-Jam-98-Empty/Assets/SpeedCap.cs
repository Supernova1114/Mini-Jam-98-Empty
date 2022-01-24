using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedCap : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField]
    private float maxSpeed = 1;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        print(rb.velocity.magnitude);
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Planet"))
        {
            StartCoroutine("PlanetState", other.attachedRigidbody);
        }
    }

    private IEnumerator PlanetState(Rigidbody body)
    {
        float temp = body.mass;

        body.mass = 0f;
        body.gameObject.SetActive(false);
        yield return new WaitForSeconds(5);
        body.gameObject.SetActive(true);
        body.mass = temp;
    }

}
