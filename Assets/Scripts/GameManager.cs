using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public GameObject gameOverUI;
    public GameObject pauseUI;
    public Text roundsFinal;
    private bool gameEnded = false;
	
    void Start() {
        gameOverUI.SetActive(false);
    }

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


    public void Retry() {
        Scene scene = SceneManager.GetActiveScene();
        string scenename = scene.name.ToString();
        //SceneManager.LoadScene(scenename);
        Application.LoadLevel(scenename);
    }

    public void Menu() {
        SceneManager.LoadScene("Menu");
    }

    public void TEE() {
        SceneManager.LoadScene("TEE");
    }


    public void Pause() {
        Time.timeScale = 0f;
        pauseUI.SetActive(true);
    }


    public void SpeedNormal() {
        Time.timeScale = 1f;
        pauseUI.SetActive(false);
    }

    public void SpeedDouble() {
        Time.timeScale = 2f;
    }
}
