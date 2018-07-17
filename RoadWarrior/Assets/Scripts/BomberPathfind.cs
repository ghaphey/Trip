using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberPathfind : MonoBehaviour {

    [SerializeField] private float speed = 10;
    [SerializeField] private float accelerate = 0.001f;
    [SerializeField] private int numLanes = 3;
    [SerializeField] private float manueverDistance = 10;
    [SerializeField] private float bomberWidth;
    [SerializeField] private float laneWidth;

    private Transform playerTransform;
    private Rigidbody rb;

    private int currLane = 2;
    private int[] coinFlip = new int[] { 1, -1 };

	// Use this for initialization
	void Start ()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
	}

    // Update is called once per frame
    void Update ()
    {
        AvoidBarriers();
        if (CheckPlayerDistance())
        {
            speed += accelerate;
            if (playerTransform.position.x != transform.position.x)
            {
                MoveToPlayerLane();
            }
        }
        rb.MovePosition(rb.position + Vector3.forward * speed * Time.deltaTime);

    }


    private bool CheckPlayerDistance()
    {
        if (Vector3.Distance(transform.position, playerTransform.position) <= manueverDistance)
        {
            return true;
        }
        else
            return false;
    }

    private void AvoidBarriers()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, manueverDistance))
        {
            if (hit.collider.tag == "Obstacle")
            {
                ChangeLane();
            }
        }
    }

    private void ChangeLane()
    {
        if (currLane == numLanes - 1)
        {
            currLane--;
        }
        else if (currLane == 0)
        {
            currLane++;
        }
        else
        {
            currLane += coinFlip[UnityEngine.Random.Range(0, 1)];
        }
        rb.MovePosition(new Vector3((currLane * laneWidth) + bomberWidth / 2, transform.position.y, transform.position.z));
    }


    private void MoveToPlayerLane()
    {
        if (playerTransform.position.x > transform.position.x)
            rb.MovePosition(rb.position + new Vector3(laneWidth, 0f, 0f));
        else
            rb.MovePosition(rb.position - new Vector3(laneWidth, 0f, 0f));

    }

    private void OnCollisionEnter(Collision collision)
    {
        print("Collided: " + collision.collider.tag);
    }
}
