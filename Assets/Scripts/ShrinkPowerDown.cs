using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class ShrinkPowerDown : MonoBehaviour
{
    public AudioClip shrinkClip;
    public GameObject player1Paddle;
    public GameObject player2Paddle;

    private AudioSource _source;
    private void Start()
    {
        _source = GetComponent<AudioSource>();
        Reposition();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Ball"))
            return;
        
        if (other.gameObject.GetComponent<PongBall>().VelocityBeforeCollision().x < 0f)
        {
            player1Paddle.gameObject.GetComponent<PaddleController>().Shrink();
        }
        else
        {
            player2Paddle.gameObject.GetComponent<PaddleController>().Shrink();
        }
        
        _source.PlayOneShot(shrinkClip);
        
        Reposition();
    }

    private void Reposition()
    {
        gameObject.SetActive(true);
        transform.position = new Vector3(Random.Range(-5f, 5f), Random.Range(-7f, 7f), 0f);
    }
}
