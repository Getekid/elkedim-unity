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
        while(isPlayerAlive)
        {
            float rx = Random.Range(-9.0f, 9.0f);
            Vector3 enemyPos = new Vector3(rx, 6.5f, 0);
            Instantiate(enemyPrefab, enemyPos, Quaternion.identity);

            yield return new WaitForSeconds(5.0f);
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
