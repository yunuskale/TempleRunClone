using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDesigner : MonoBehaviour
{
    public Transform[] spawnPositions;
    public GameObject[] obstacles;
    void Start()
    {
        for(int i = 0; i < spawnPositions.Length; i+=3)
        {
            int s = Random.Range(0,3);
            int r = Random.Range(0, 10);
            int o = Random.Range(0, obstacles.Length);
            if(r>8)
            {
                GameObject obstacle = Instantiate(obstacles[o]);
                obstacle.transform.position = new Vector3(spawnPositions[i+s].position.x, obstacle.transform.position.y, spawnPositions[i + s].position.z);
            }
        }
    }

}
