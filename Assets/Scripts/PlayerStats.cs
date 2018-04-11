using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour {

    [Header("Character Attributes")]
    public static int money;
    public static int lives;

    [Header("Start Values")]
    public int startMoney = 500;
    public Text moneyText;
    public int startLives = 20;
    public Text livesUI;
    public GameObject livesUIObject;

    [Header("Reference")]
    public static int round;

    void Start() {
        money = startMoney;
        moneyText.text = money.ToString();

        lives = startLives;
        livesUI.text = lives.ToString();

        round = 0;
    }

    void Update() {
        moneyText.text = money.ToString();
        //moneyText.GetComponent<Text>().text = PlayerStats.money.ToString();
    }


    // Update Text abstract
    public void SetPlayerStat(GameObject elementToChange) {
        elementToChange.GetComponent<Text>().text = lives.ToString();
    }


    public void SubtractLife() {
        lives--;
        SetPlayerStat(livesUIObject);

        // check for life 0
        if (lives <= 0) {
            Debug.Log("game over");
        }
    }


    public void UpdateMoney() {
        moneyText.text = money.ToString();
    }


}
