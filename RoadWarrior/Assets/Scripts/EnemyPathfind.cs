using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathfind : MonoBehaviour {

    [SerializeField] private float speed = 10;
    [SerializeField] private int numLanes = 3;
    [SerializeField] private float bomberWidth;
    [SerializeField] private float laneWidth;

    private int currLane = 3;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += Vector3.forward * speed * Time.deltaTime;

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
        transform.position = new Vector3((currLane * laneWidth) + bomberWidth / 2, transform.position.y, transform.position.z);
    }
}
