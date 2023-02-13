using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

public class PaddleController : MonoBehaviour
{
    public float unitsPerSecond;
    public AudioClip ballSound;
    public AudioClip shrinkSound;

    private Rigidbody _rigidbody;
    private string _playerVerticalAxis;
    private float _lengthY;
    private bool _isShrunken;
    private AudioSource _source;

    private static int _timesHit;
    private const float MaxAngle = 60f;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _playerVerticalAxis = CompareTag("Player1") ? "P1Vertical" : "P2Vertical";
        _lengthY = GetComponent<BoxCollider>().bounds.size.y;
        _source = GetComponent<AudioSource>();
        ResetTimesHit();
    }

    private void FixedUpdate()
    {
        float verticalAxis = Input.GetAxis(_playerVerticalAxis);
        _rigidbody.velocity = verticalAxis * unitsPerSecond * Vector3.up;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Ball"))
            return;

        Bounds bounds = GetComponent<BoxCollider>().bounds;
        Rigidbody ballRigidBody = collision.gameObject.GetComponent<Rigidbody>();
        Vector3 ballVelocity = collision.gameObject.GetComponent<PongBall>().VelocityBeforeCollision();
        //Debug.Log($"Hit Paddle, Current total Velocity = {ballVelocity.magnitude}");
        
        float magnitude = ballVelocity.magnitude * (1f + _timesHit/100f);
        float collisionTransformY = Mathf.Clamp(collision.transform.position.y, bounds.min.y, bounds.max.y);
        
        float rotationScaleY = (2f * (collisionTransformY - bounds.min.y) / _lengthY) - 1f;
        
        if (ballVelocity.x > 0f)
            ballRigidBody.velocity = Quaternion.Euler(0f, 0f, rotationScaleY * -MaxAngle) * Vector3.left * magnitude;
        else
            ballRigidBody.velocity = Quaternion.Euler(0f, 0f, rotationScaleY * MaxAngle) * Vector3.right * magnitude;
        
        _timesHit++;
        PlayBallSound();
        if (_isShrunken)
            Grow();
    }

    private void Grow()
    {
        Debug.Log("growing");
        _isShrunken = false;
        transform.localScale = new Vector3(1f, 5f, 1f);
    }

    public void Shrink()
    {
        Debug.Log("shrinking");
        _isShrunken = true;
        transform.localScale = new Vector3(1f, 2.5f, 1f);
    }

    private void PlayBallSound()
    {
        _source.outputAudioMixerGroup.audioMixer.SetFloat("BouncePitch", (0.85f + _timesHit / 100f));
        _source.PlayOneShot(ballSound);
    }

    public static void ResetTimesHit()
    {
        _timesHit = 0;
    }
}