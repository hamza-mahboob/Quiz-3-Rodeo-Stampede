using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] animals;
    public GameObject obstacle;

    float xBound = 23;
    float zBoundLow = 50;
    float zBoundHigh = 90;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnRandomAnimal", 2, 5);
        InvokeRepeating("SpawnObstacle", 2, 6);
    }

    // Update is called once per frame
    void Update()
    {

    }

    Vector3 GenerateRandomPostion()
    {
        //random x, z
        float randomX = Random.Range(-xBound, xBound);
        float randomZ = Random.Range(zBoundLow, zBoundHigh);

        //random spawn position
        return new Vector3(randomX, 0, randomZ);
    }

    void SpawnRandomAnimal()
    {
        //offset
        Vector3 offset = new Vector3(0, 2.75f, 0);

        //random animal
        int randomIndex = Random.Range(0, animals.Length);

        //instantiate animal
        Instantiate(animals[randomIndex], GenerateRandomPostion() + offset, animals[randomIndex].transform.rotation);
    }

    void SpawnObstacle()
    {
        //offset
        Vector3 offset = new Vector3(0, 2.75f, 0);
        //instantiate obstacle
        Instantiate(obstacle, GenerateRandomPostion() + offset, obstacle.transform.rotation);
    }
}
