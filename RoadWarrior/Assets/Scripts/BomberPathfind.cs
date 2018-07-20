using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberPathfind : MonoBehaviour {

    //[SerializeField] private float speed = 10;
    //[SerializeField] private float accelerate = 0.001f;
    [SerializeField] private int numLanes = 3;
    [SerializeField] private float manueverDistance = 10;
    [SerializeField] private float bomberWidth;
    [SerializeField] private float laneWidth;
    [SerializeField] private int currLane = 2;
    [SerializeField] private EnemyWheelController wheelController;

    private Transform playerTransform;
    //private Rigidbody rb;
    public Vector3 targetLine;
    private int timeTurning;
    private string avoidingName;

	// Use this for initialization
	void Start ()
    {
        targetLine = transform.position;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        //rb = GetComponent<Rigidbody>();
        wheelController.SetMotorTorque(wheelController.maxMotorTorque);
	}

    // Update is called once per frame
    void Update ()
    {
        AvoidBarriers();
        if (CheckPlayerDistance())
        {
            if (playerTransform.position.x != transform.position.x)
            {
                DriveAtPlayer();
            }
        }
        //rb.MovePosition(rb.position + Vector3.forward * speed * Time.deltaTime);

        if (transform.position.x - wheelController.lineFollowDeviation > targetLine.x )
        {
            wheelController.SetSteeringAngle(-wheelController.turnRate);
            //print("turningleft");
            //timeTurning--;
        }
        else if (transform.position.x + wheelController.lineFollowDeviation < targetLine.x )
        {
            wheelController.SetSteeringAngle(wheelController.turnRate);
            //print("turningright");
            //timeTurning++;
        }
        else
        {
            wheelController.SetSteeringAngle(0.0f);
            //if (timeTurning > 0)
            //{
            //    SetSteeringAngle(-turnRate);
            //}
            //else if (timeTurning < 0)
            //{
            //    SetSteeringAngle(turnRate);
            //}
            //else
            //    SetSteeringAngle(0.0f);
        }
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
            if (hit.collider.tag == "Obstacle" && avoidingName != hit.collider.name)
            {
                ChangeLane();
                avoidingName = hit.collider.name;
                print("Trying to avoid: " + avoidingName);
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
            int tmp = Mathf.FloorToInt(UnityEngine.Random.value * 10) % 2;
            if (tmp == 0)
                tmp = -1;
            currLane += tmp;
        }
        targetLine = new Vector3((currLane * laneWidth) + bomberWidth, 
                                 transform.position.y, 
                                 transform.position.z);
    }


    private void DriveAtPlayer()
    {
        targetLine = playerTransform.position;

    }

    private void OnCollisionEnter(Collision collision)
    {
        //print("Collided: " + collision.collider.tag);
    }
}
