using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [SerializeField] private GameObject bomberPrefab;
    [SerializeField] private float roadWidth = 6f;
    [SerializeField] private int numLanes = 3;
    [SerializeField] private float spawnOffset = 10f;
    [SerializeField] private Transform playerPos;

    private bool enemiesNotSpawned = true;

    // Use this for initialization

    private void Start()
    {
        if (playerPos == null)
            playerPos = GameObject.FindGameObjectWithTag("Player").transform;        
    }


    // Update is called once per frame
    void Update ()
    {
		if (playerPos.position.z - spawnOffset > transform.position.z && enemiesNotSpawned)
        {
            int numSpawning = Mathf.FloorToInt(UnityEngine.Random.Range(0, numLanes));
            int i = 0;
            do
            {
                Instantiate(bomberPrefab, transform.localPosition + new Vector3(1 + i * 2, 0.1f, 5f),
                    Quaternion.identity);
                i++;
            } while (i < numLanes);
            enemiesNotSpawned = false;
        }
	}
}
