using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    private Rigidbody playerRB;

    [SerializeField]
    private float distanceFactor = 1;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = player.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = -(playerRB.velocity.normalized * distanceFactor) + player.transform.position;
    }
}
