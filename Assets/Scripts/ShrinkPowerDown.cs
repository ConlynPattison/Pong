using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShrinkPowerDown : MonoBehaviour
{
    public GameObject player1Paddle;
    public GameObject player2Paddle;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Ball"))
            return;
        
        if (other.gameObject.GetComponent<PongBall>().VelocityBeforeCollision().x < 0f)
        {
            player1Paddle.gameObject.GetComponent<PongPaddle>().Shrink();
        }
        else
        {
            player2Paddle.gameObject.GetComponent<PongPaddle>().Shrink();
        }
        
        Reposition();
    }

    private void Reposition()
    {
        return;
    }
}
