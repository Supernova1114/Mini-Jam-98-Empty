using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerVelocity : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    private Rigidbody playerRB;

    [SerializeField]
    private float distanceFactor = 1;

    public bool shouldFollow = true;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = player.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldFollow)
        {
            Vector3 temp = -(playerRB.velocity.normalized * distanceFactor) + player.transform.position;

            Vector3 velocity = playerRB.velocity;

            if (velocity.magnitude > 0)
            {
                transform.forward = velocity;
            }

            transform.position = temp;
        }
        
    }
}
