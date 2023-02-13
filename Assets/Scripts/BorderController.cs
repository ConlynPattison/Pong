using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PongBorder : MonoBehaviour
{
    public GameObject ballSpawner;
    public AudioClip ballSound;
    
    private AudioSource _source;

    private void Start()
    {
        _source = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Ball"))
            return;
        Vector3 velocity = collision.gameObject.GetComponent<PongBall>().VelocityBeforeCollision();
        collision.rigidbody.velocity = new Vector3(velocity.x, velocity.y * -1, velocity.z);
        
        PlayBallSound();
        
        //Debug.Log($"Hit Border, Current total Velocity = {velocity.magnitude}");
    }

    private void OnCollisionStay(Collision collisionInfo)
    {
        if (!collisionInfo.gameObject.CompareTag("Ball"))
            return;
        if (collisionInfo.gameObject.GetComponent<Rigidbody>().velocity.y == 0f)
        {
            Destroy(collisionInfo.gameObject);
            ballSpawner.GetComponent<BallSpawnerController>().NextServe();
            PaddleController.ResetTimesHit();
        }
    }
    
    private void PlayBallSound()
    {
        _source.PlayOneShot(ballSound);
    }
}
