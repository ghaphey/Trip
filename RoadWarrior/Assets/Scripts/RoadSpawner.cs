using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSpawner : MonoBehaviour {

    [SerializeField] private GameObject roadPrefab;
    [SerializeField] private Transform playerPos;
    [SerializeField] private float roadLength = 100f;
    [SerializeField] private float despawnDistance = 100f;

    private Queue<GameObject> currRoads = new Queue<GameObject>();
    private int numRoads = 1;

	// Use this for initialization
	void Start () {
        currRoads.Enqueue(transform.GetChild(0).gameObject);
        currRoads.Enqueue(AddRoadSegment());
        currRoads.Enqueue(AddRoadSegment());
    }
	
	// Update is called once per frame
	void Update () {
		if(playerPos.position.z > currRoads.Peek().transform.position.z + despawnDistance)
        {
            Destroy(currRoads.Dequeue());
            currRoads.Enqueue(AddRoadSegment());
        }

	}

    private GameObject AddRoadSegment()
    {
        GameObject newRoad = Instantiate(roadPrefab, transform, false);
        newRoad.transform.localPosition = new Vector3(0.0f, 0.0f, roadLength * numRoads++);
        print(numRoads);
        return newRoad;
    }
}
