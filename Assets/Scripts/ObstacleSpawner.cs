using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstacle;
    public Transform lowPos;
    public Transform midPos;
    public Transform highPos;
    
    public float spawnTime = 10.0f;
    public float spawnTimer = 0.0f;

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.Instance.isPlayerDead)
        {
            spawnTimer += Time.deltaTime;

            if (spawnTimer >= spawnTime)
            {
                int num = Random.Range(0, 2);
                Debug.Log("Spawned: " + num);
                spawnTimer = 0.0f;

                if (num == 0)
                {
                    Vector3 spawnPos = lowPos.position;
                    Instantiate(obstacle, spawnPos, Quaternion.identity);
                }
                else if (num == 1)
                {
                    Vector3 spawnPos = midPos.position;
                    Instantiate(obstacle, spawnPos, Quaternion.identity);
                }
                else
                {
                    Vector3 spawnPos = highPos.position;
                    Instantiate(obstacle, spawnPos, Quaternion.identity);
                }
            }
        }
    }
}
