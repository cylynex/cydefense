﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public GameObject gameOverUI;
    public Text roundsFinal;
    private bool gameEnded = false;
	
	void Update () {
        if (gameEnded) return;

        if (PlayerStats.lives <= 0) {
            EndGame();
        }
	}

    void EndGame() {
        // Set Game Over and pop screen
        gameEnded = true;
        gameOverUI.SetActive(true);

        // Disable Spawner
        GetComponent<WaveSpawner>().enabled = false;
        Camera.main.GetComponent<CameraController>().enabled = false;
        roundsFinal.text = PlayerStats.round.ToString();

        // Clear all Enemies

    }
}
