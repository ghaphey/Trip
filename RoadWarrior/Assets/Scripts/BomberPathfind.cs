using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberPathfind : MonoBehaviour {

    //[SerializeField] private float speed = 10;
    //[SerializeField] private float accelerate = 0.001f;
    //[SerializeField] private int numLanes = 3;
    [SerializeField] private float manueverDistance = 10;
    [SerializeField] private float initialBoost = 25000f;
    //[SerializeField] private float bomberWidth;
    //[SerializeField] private float laneWidth;
    //[SerializeField] private int currLane = 2;
    [SerializeField] private EnemyWheelController wheelController;

    private Transform playerTransform;
    public Vector3 targetLine;
    private int timeTurning;
    private string avoidingName;

	// Use this for initialization
	void Start ()
    {
        targetLine = transform.position;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        GetComponent<Rigidbody>().AddForce(Vector3.forward * initialBoost, ForceMode.Impulse);
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
            if (hit.collider.tag == "Obstacle" && avoidingName != hit.collider.transform.parent.name)
            {
                ChangeLane(hit.collider.GetComponent<ObstacleProperties>().GetSafeZones());
                avoidingName = hit.collider.transform.parent.name;
                //print("Trying to avoid: " + avoidingName);
            }
        }
    }

    private void ChangeLane(List<Transform> safe)
    {
        targetLine = safe[Mathf.FloorToInt(UnityEngine.Random.Range(0, safe.Count))].position;
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
