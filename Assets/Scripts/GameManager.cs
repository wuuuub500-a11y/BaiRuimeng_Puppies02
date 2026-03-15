using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public bool gameStarted;
    public GameObject frisbee;
    private float spawnTimer = 2f;

    private void Update()
    {
        if(!gameStarted)
        {
            return;
        }
        spawnTimer -= Time.deltaTime;
        {
            if (spawnTimer <= 0)
            {
                SpawnFrisbee();
                spawnTimer = 2f;
            }
        }
    }


    public void SpawnFrisbee()
    {
        float randomX = Random.Range(-6f, 6f);
        Instantiate(frisbee, new Vector3(randomX, 0f, 0f), Quaternion.identity);
    }
    
    
}
