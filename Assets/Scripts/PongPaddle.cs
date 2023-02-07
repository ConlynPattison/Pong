using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.SceneManagement;
using UnityEngine;

public class PongPaddle : MonoBehaviour
{
    public float unitsPerSecond;
    public AudioSource audioSource;

    private Rigidbody _rigidbody;
    private string _playerVerticalAxis;
    private float _lengthY;

    private static int _timesHit;
    private const float MaxAngle = 60f;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _playerVerticalAxis = CompareTag("Player1") ? "P1Vertical" : "P2Vertical";
        _lengthY = GetComponent<BoxCollider>().bounds.size.y;
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
        Debug.Log($"Hit Paddle, Current total Velocity = {ballVelocity.magnitude}");
        
        float magnitude = ballVelocity.magnitude * (1f + _timesHit/100f);
        float collisionTransformY = collision.transform.position.y;
        
        if (collisionTransformY > bounds.max.y)
            collisionTransformY = bounds.max.y;
        else if (collisionTransformY < bounds.min.y)
            collisionTransformY = bounds.min.y;
        
        float rotationScaleY = (2f * (collisionTransformY - bounds.min.y) / _lengthY) - 1f;
        
        if (ballVelocity.x > 0f)
            ballRigidBody.velocity = Quaternion.Euler(0f, 0f, rotationScaleY * -MaxAngle) * Vector3.left * magnitude;
        else
            ballRigidBody.velocity = Quaternion.Euler(0f, 0f, rotationScaleY * MaxAngle) * Vector3.right * magnitude;
        
        _timesHit++;
        audioSource.pitch = (0.85f + _timesHit / 100f);
        audioSource.Play();
    }

    public static void ResetTimesHit()
    {
        _timesHit = 0;
    }
}