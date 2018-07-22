using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierSpawner : MonoBehaviour {

    [Header("Spawn Surface Properties")]
    [SerializeField] private float width = 6f;
    [SerializeField] private int numLanes = 3;
    [SerializeField] private float length = 100f;
    [Header("Spawn Properties")]
    [SerializeField] private List<GameObject> obstaclePrefabs;
    [SerializeField] private float objectMax = 20;
    [SerializeField] private float objectIntervalMin = 15f;
    [SerializeField] private float objectIntervalMax = 30f;

    private float curLocation = 0.0f;

	// Use this for initialization
	void Start () {
        SpawnObjects(Mathf.CeilToInt(UnityEngine.Random.Range(objectMax / 2, objectMax)));
	}

    private void SpawnObjects(int numSpawn)
    {

        int spawned = 0;
        while (spawned < numSpawn)
        {
            curLocation += UnityEngine.Random.Range(objectIntervalMin, objectIntervalMax);
            if (curLocation < length)
            {
                Instantiate(obstaclePrefabs[Mathf.FloorToInt(UnityEngine.Random.Range(0, obstaclePrefabs.Count))],
                            new Vector3(0.0f, 0.5f, curLocation),
                            Quaternion.identity,
                            transform);
            }
            else
                return;
            spawned++;
        }
    }
}
