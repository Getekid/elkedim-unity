using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // The enemy object.
    [SerializeField]
    private GameObject enemyPrefab = null;
    // The triple shot object.
    [SerializeField]
    private GameObject[] powerups;

    // Boolean wheather the player is alive or dead.
    private bool isPlayerAlive = true;

    // Variables to control the enemy spawn rate.
    private float enemySpawnRate = 5.0f;
    private float enemySpawnRateMin = 4.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnRoutineEnemy());
        StartCoroutine(SpawnRoutinePowerup());
    }

    IEnumerator SpawnRoutineEnemy()
    {
        int enemiesSpawned = 0;
        while(isPlayerAlive)
        {
            Vector3 enemyPos = new Vector3(Random.Range(-9.0f, 9.0f), 6.5f, 0);
            Instantiate(enemyPrefab, enemyPos, Quaternion.identity);
            enemiesSpawned++;

            // Lower the rate by 0.1 for every 10 enemy spawns until we reach the minimum.
            if (enemiesSpawned % 10 == 0 && enemySpawnRate > enemySpawnRateMin)
            {
                enemySpawnRate -= 0.1f;
            }

            yield return new WaitForSeconds(enemySpawnRate);
        }
    }

    IEnumerator SpawnRoutinePowerup()
    {
        while(isPlayerAlive)
        {
            float rx = Random.Range(-9.0f, 9.0f);
            Vector3 powerupPos = new Vector3(rx, 6.5f, 0);

            int randomPowerup = Random.Range(0, 3);
            Instantiate(powerups[randomPowerup], powerupPos, Quaternion.identity);

            yield return new WaitForSeconds(Random.Range(10, 16));
        }
    }

    public void PlayerDied()
    {
        isPlayerAlive = false;
    }
}
