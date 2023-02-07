using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class ScoreZone : MonoBehaviour
{
    public GameObject ballSpawner;
    public TextMeshProUGUI scoreTM;
    public TextMeshProUGUI winTM;
    
    private int _playerScore;
    private const int GoalScore = 11;
    private static bool _serveRight;

    private void Start()
    {
        _playerScore = 0;
        _serveRight = Random.Range(0, 2) == 1;
        
        UpdateScoreText();
        winTM.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Ball"))
            return;
        
        PlayerScored();
        Destroy(other.gameObject);
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
        {
            PongPaddle.ResetTimesHit();
            _serveRight = CompareTag("Player1");
            ballSpawner.GetComponent<BallSpawnerController>().NextRound();
        }
        else
        {
            winTM.text = $"{tag} Wins!";
            winTM.gameObject.SetActive(true);
        }
        

    }
    private void UpdateScoreText()
    {
        scoreTM.text = $"{_playerScore}";
    }

    public void Restart()
    {
        _playerScore = 0;
        UpdateScoreText();
    }
}
