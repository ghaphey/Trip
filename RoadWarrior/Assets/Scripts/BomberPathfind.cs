using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberPathfind : MonoBehaviour {

    //[SerializeField] private float speed = 10;
    //[SerializeField] private float accelerate = 0.001f;
    [Header("Distances")]
    [SerializeField] private int numLanes = 3;
    [SerializeField] private float manueverDistance = 10;
    [SerializeField] private float bomberWidth;
    [SerializeField] private float laneWidth;
    [Header("Wheel Information")]
    [SerializeField] private List<AxleInfo> axleInfos;
    [SerializeField] private float maxMotorTorque;
    [SerializeField] private float maxSteeringAngle;
    [SerializeField] private float turnRate = 0.25f;
    [SerializeField] private float lineFollowDeviation = 0.5f;
    [SerializeField] private List<Transform> wheelModels;

    private Transform playerTransform;
    //private Rigidbody rb;
    private Vector3 targetLine;
    private float currSteerAngle = 0.0f;
    private int timeTurning;

    private int currLane = 2;
    private int[] coinFlip = new int[] { 1, -1 };

	// Use this for initialization
	void Start ()
    {
        targetLine = transform.position;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        //rb = GetComponent<Rigidbody>();
        SetMotorTorque(maxMotorTorque);
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

        if (transform.position.x - lineFollowDeviation > targetLine.x || timeTurning > 0)
        {
            SetSteeringAngle(-turnRate);
            print("turningleft");
            timeTurning--;
        }
        else if (transform.position.x + lineFollowDeviation < targetLine.x || timeTurning < 0)
        {
            SetSteeringAngle(turnRate);
            print("turningright");
            timeTurning++;
        }
        else
        {
            SetSteeringAngle(0.0f);
        }
    }

    private void SetSteeringAngle(float angle)
    {
        if (angle != 0f)
            currSteerAngle = Mathf.Clamp(currSteerAngle + angle * Time.deltaTime, -maxSteeringAngle, maxSteeringAngle);
        else
            currSteerAngle = 0.0f;
        int i = 0;
        foreach (AxleInfo axleInfo in axleInfos)
        {
            if (axleInfo.steering)
            {
                axleInfo.leftWheel.steerAngle = currSteerAngle;
                axleInfo.rightWheel.steerAngle = currSteerAngle;
            }
            ApplyLocalPositionToVisuals(axleInfo.leftWheel, i++);
            ApplyLocalPositionToVisuals(axleInfo.rightWheel, i++);
        }
    }

    private void SetMotorTorque(float motorTorque)
    {
        foreach (AxleInfo axleInfo in axleInfos)
        {
            if (axleInfo.motor)
            {
                axleInfo.leftWheel.motorTorque = motorTorque;
                axleInfo.rightWheel.motorTorque = motorTorque;
            }
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
            if (hit.collider.tag == "Obstacle")
            {
                print("Trying to change lane");
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
        targetLine = new Vector3((currLane * laneWidth) + bomberWidth / 2, 
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

    private void ApplyLocalPositionToVisuals(WheelCollider collider, int wheelIndex)
    {

        Transform visualWheel = wheelModels[wheelIndex];

        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);

        visualWheel.transform.position = position;
        visualWheel.transform.rotation = rotation;
    }
}
