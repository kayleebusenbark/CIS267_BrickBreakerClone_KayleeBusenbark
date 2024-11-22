using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickSpawner : MonoBehaviour
{
    public GameObject[] spawnedObjects;
    public List<GameObject> spawnLocations = new List<GameObject>();
    public GameObject[] powerUps;

    private int brickCount = 0;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawnBrick()
    {
        foreach (GameObject spawnLocation in spawnLocations)
        {
            int randomBrick = Random.Range(0, spawnedObjects.Length);
            GameObject brick = Instantiate(spawnedObjects[randomBrick], spawnLocation.transform.position, Quaternion.identity);

            brickCount++;

            //there's a 10% chance the brick "holds" a power up
            if (Random.value > 0.8f)
            {
                int randomPowerUp = Random.Range(0, powerUps.Length);
                brick.GetComponent<BrickHealth>().powerUpPrefab = powerUps[randomPowerUp];
            }
        }

        GameManager gameManager = FindObjectOfType<GameManager>();
        gameManager.addBrickToCount(brickCount);

    }
}
