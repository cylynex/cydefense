using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave {

    // Enemy for this wave
    public GameObject enemyPrefab;

    // Amount of enemies this wave will spawn
    public int enemyAmount;

    // Time between each mob spawning
    public float spawnRate;

    // Modifier to the "until next wave" timer - useful for very fast or slow mobs or pace changes unexpectedly
    public float spawnWaveTimer = 10f;
	
}
