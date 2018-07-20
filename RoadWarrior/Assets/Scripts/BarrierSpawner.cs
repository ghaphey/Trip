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
        curLocation += UnityEngine.Random.Range(objectIntervalMin, objectIntervalMax);

        if (curLocation < length)
        {
            // PICK A RANDOM OBSTACLE AND PLACE IT
        }
    }
}
