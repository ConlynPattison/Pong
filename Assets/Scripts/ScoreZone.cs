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
    public GameObject gameManager;
    public TextMeshProUGUI scoreTM;
    public AudioClip scoreClip;

    private AudioSource _source;
    private int _playerScore;
    private const int GoalScore = 11;
    private static bool _serveRight;

    private void Start()
    {
        _source = GetComponent<AudioSource>();
        Reset();
    }
    
    public void Reset()
    {
        _playerScore = 0;
        InitServeDirection();
        scoreTM.text = $"{_playerScore}";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Ball"))
            return;
        
        _source.PlayOneShot(scoreClip);
        PlayerScored();
        Destroy(other.gameObject);
    }

    private void PlayerScored()
    {
        // String playerTag = tag;
        // Debug.Log($"{playerTag} scores, {playerTag} has {++_playerScore} points");
        ++_playerScore;
        UpdateScoreText();

        if (_playerScore != GoalScore)
        {
            _serveRight = CompareTag("Player1");
            ballSpawner.GetComponent<BallSpawnerController>().NextServe();
        }
        else
        {
            // Debug.Log($"{playerTag} has WON!! Restarting the game...");
            gameManager.GetComponent<GameManager>().PlayerWon(tag);
        }
        PaddleController.ResetTimesHit();
    }
    
    private void UpdateScoreText()
    {
        scoreTM.text = $"{_playerScore}";
        LeanTween.rotate(scoreTM.rectTransform, 360f, 0.3f);
    }

    public static bool WillServeRight()
    {
        return _serveRight;
    }
    
    private static void InitServeDirection()
    {
        _serveRight = Random.Range(0, 2) == 1;
    }
}
