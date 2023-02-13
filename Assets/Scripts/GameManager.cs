using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class GameManager : MonoBehaviour
{
    public GameObject p1ScoreZone;
    public GameObject p2ScoreZone;
    public GameObject ballSpawner;
    public TextMeshProUGUI winText;
    public Button startButton;

    private void Start()
    {
        SetUIState(false);
    }

    public void PlayerWon(string winningTag)
    {
        winText.text = $"{winningTag} has won!";
        SetUIState(true);
    }

    public void StartButtonCall()
    {
        SetUIState(false);
        RestartGame();
        ballSpawner.GetComponent<BallSpawnerController>().NextServe();
    }

    private void RestartGame()
    {
        p1ScoreZone.GetComponent<ScoreZone>().Reset();
        p2ScoreZone.GetComponent<ScoreZone>().Reset();
    }

    private void SetUIState(bool shouldShow)
    {
        startButton.gameObject.SetActive(shouldShow);
        winText.gameObject.SetActive(shouldShow);
    }
}
