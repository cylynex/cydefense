using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WaveSpawner : MonoBehaviour {

    public static int enemiesAlive = 0;

    public Transform enemyPrefab;
    public float timeBetweenWaves = 5.9f;
    public Transform spawnPoint;
    public Text waveText;
    public GameObject winUI;

    // Waves list
    public Wave[] waves;

    // Time to spawn first wave
    private float countdown = 3f;

    // wave number
    private int waveIndex = 0;
    public int totalWaves;
    public int currentWave = 0;


    // Time between mob spawns of each wave
    public float delayBetweenMobs = 0.5f;

    public Text countdownTimerLabel;

    void Start() {
        waveIndex = 0;
        totalWaves = waves.Length;
        currentWave = 0;
    }

    void Update() {
        
        if (enemiesAlive > 0) {
            return;
        }

        if (countdown <= 0) {
            
            if (currentWave == totalWaves) {
                countdown = 0;
                // done with waves, if enemies are all dead win.
                if (enemiesAlive == 0) {
                    winUI.SetActive(true);
                }
            } else {
                // only spawn if we have another level to go to
                StartCoroutine(SpawnWave());
                countdown = timeBetweenWaves;
                return;
            }
        }

        countdown -= Time.deltaTime;
        //countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
        //countdownTimerLabel.text = Mathf.Floor(countdown + 1).ToString();
        countdownTimerLabel.text = string.Format("{0:00.00}", countdown);
    }

    // Start next wave
    IEnumerator SpawnWave() {

        currentWave++;
        Debug.Log("Wave Situtation: " + currentWave + "/" + totalWaves);

        PlayerStats.round++;
        waveText.text = PlayerStats.round.ToString();

        Wave wave = waves[waveIndex];

        for (int i = 0; i < wave.enemyAmount; i++) {
            SpawnEnemy(wave.enemyPrefab);
            yield return new WaitForSeconds(1f / wave.spawnRate);
        }

        waveIndex++;

     }


    // Spawn mob
    void SpawnEnemy(GameObject enemyToSpawn) {
        GameObject thisEnemy = Instantiate(enemyToSpawn, spawnPoint.position, spawnPoint.rotation);
        thisEnemy.transform.SetParent(this.transform,true);

        // Add to enemies alive for tracking
        enemiesAlive++;
    }

}
