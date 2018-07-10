using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int health = 100;
    [SerializeField] private float maxSpeed = 10;
    [SerializeField] private float minSpeed = 5;
    [SerializeField] private float laneWidth = 2.0f;
    [SerializeField] private float carWidth = 1.0f;
    [SerializeField] private int numLanes = 3;
    [SerializeField] private Transform cameraPivot;
    [SerializeField] private Transform turretBase;

    private float currSpeed = 0.0f;
    private int currLane = 0;

    private void Start()
    {
        currSpeed = minSpeed;
    }

    private void Update()
    {
        transform.position += Vector3.forward * currSpeed * Time.deltaTime;
        ChangeLane();
        MoveCamera();
        turretTrackMouse();
    }

    private void ChangeLane()
    {
        if (Input.GetKeyDown("a"))
        {
            if (currLane > 0)
            {
                currLane--;
            }
        }
        else if (Input.GetKeyDown("d"))
        {
            if (currLane < numLanes - 1)
            {
                currLane++;
            }
        }
        transform.position = new Vector3((currLane * laneWidth) + carWidth / 2, transform.position.y, transform.position.z);
    }

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




}
