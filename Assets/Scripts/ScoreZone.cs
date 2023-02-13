using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class ScoreZone : MonoBehaviour
{
    public GameObject ballSpawner;
    public GameObject gameManager;
    public TextMeshProUGUI scoreTM;
    
    private int _playerScore;
    private const int GoalScore = 11;
    private static bool _serveRight;

    private void Start()
    {
        Reset();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Ball"))
            return;
        
        PlayerScored();
        Destroy(other.gameObject);
    }

    private static void InitServeDirection()
    {
        _serveRight = Random.Range(0, 2) == 1;
    }

    public static bool WillServeRight()
    {
        return _serveRight;
    }

    private void PlayerScored()
    {
        String playerTag = tag;
        Debug.Log($"{playerTag} scores, {playerTag} has {++_playerScore} points");
        UpdateScoreText();
        
        if (_playerScore != GoalScore)
            _serveRight = CompareTag("Player1");
        else
        {
            Debug.Log($"{playerTag} has WON!! Restarting the game...");
            gameManager.GetComponent<WinCondition>().PlayerWon();
        }
        ballSpawner.GetComponent<BallSpawnerController>().NextRound();
        PaddleController.ResetTimesHit();
    }
    
    private void UpdateScoreText()
    {
        scoreTM.text = $"{_playerScore}";
    }

    public void Reset()
    {
        _playerScore = 0;
        InitServeDirection();
        UpdateScoreText();
    }
}
