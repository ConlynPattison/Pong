using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReversePowerUp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Ball"))
            return;
        
        Vector3 ballVelocity = other.GetComponent<Rigidbody>().velocity;
        other.GetComponent<Rigidbody>().velocity = new Vector3(-ballVelocity.x, ballVelocity.y, ballVelocity.z);
        
        Relocate();
    }

    private void Relocate()
    {
        // todo
        // Move the power up to a new location after it has been used
    }
}
