using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int health = 100;
    //[SerializeField] private float maxSpeed = 10;
    //[SerializeField] private float minSpeed = 5;
    //[SerializeField] private float laneWidth = 2.0f;
    //[SerializeField] private float carWidth = 1.0f;
    //[SerializeField] private float turretPitchMax = 45.0f;
    //[SerializeField] private float turretYawMax = 45.0f;
    //[SerializeField] private float maxRange = 20.0f;
    //[SerializeField] private int numLanes = 3;
    [SerializeField] private int cannonDamage = 10;
    [SerializeField] private Transform cameraPivot;
    [SerializeField] private Transform turretBase;
    [SerializeField] private Transform barrel;
    [SerializeField] private GameObject hitFX;
    [SerializeField] private ParticleSystem fireFX;

   // private float currSpeed = 0.0f;
    //private int currLane = 0;

    private Rigidbody rb;
    public Vector3 COM;

    private void Start()
    {
        //currSpeed = minSpeed;
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = COM;
    }

    private void Update()
    {
        if (health <= 0)
            print("GameOver");
        //transform.position += Vector3.forward * currSpeed * Time.deltaTime;
       //ChangeLane();
        MoveCamera();
        turretTrackMouse();

        if (Input.GetMouseButtonDown(0))
        {
            FireCannon();
        }


    }

    //private void ChangeLane()
    //{
    //    if (Input.GetKeyDown("a"))
    //    {
    //        if (currLane > 0)
    //        {
    //            currLane--;d
    //        }
    //    }
    //    else if (Input.GetKeyDown("d"))
    //    {
    //        if (currLane < numLanes - 1)
    //        {
    //            currLane++;
    //        }
    //    }
    //    transform.position = new Vector3((currLane * laneWidth) + carWidth / 2, transform.position.y, transform.position.z);
    //}

    private void MoveCamera()
    {
        if(Input.GetKeyDown("e"))
        {
            cameraPivot.Rotate(0.0f, 90.0f, 0.0f);
            turretBase.Rotate(0.0f, 90.0f, 0.0f);
        }
        else if (Input.GetKeyDown("q"))
        {
            cameraPivot.Rotate(0.0f, -90.0f, 0.0f);
            turretBase.Rotate(0.0f, -90.0f, 0.0f);
        }
    }



    private void turretTrackMouse()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            turretBase.GetChild(0).LookAt(hit.point);
        }
    }


    private void FireCannon()
    {
        fireFX.Play();
        RaycastHit hit;
        if(Physics.Raycast(barrel.position, barrel.TransformDirection(Vector3.up), out hit ))
        {
            Instantiate(hitFX, hit.point, Quaternion.identity);
            if (hit.transform.GetComponent<EnemyHealth>())
                hit.transform.GetComponent<EnemyHealth>().ApplyDamage(cannonDamage);
        }
    }

    public void ApplyDamage (int damage)
    {
        health -= damage;
        print(damage + " damage taken");
    }
}
