using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {

    public Transform enemyPrefab;
    public float timeBetweenWaves = 5f;
    public Transform spawnPoint;

    // Time to spawn first wave
    private float countdown = 3f;

    // wave number
    private int waveIndex = 0;

    // Time between mob spawns of each wave
    public float delayBetweenMobs = 0.5f;

    void Update() {
        if (countdown <= 0) {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }

        countdown -= Time.deltaTime;
        // Debug.Log("countdown: " + countdown);
    }

    // Start next wave
    IEnumerator SpawnWave() {

        waveIndex++;

        for (int i = 0; i < waveIndex; i++) {
            SpawnEnemy();
            yield return new WaitForSeconds(delayBetweenMobs);
        }
     }


    // Spawn mob
    void SpawnEnemy() {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }

}
