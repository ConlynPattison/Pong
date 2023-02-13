using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawnerController : MonoBehaviour
{
    public GameObject ballPrefab;

    public void NextServe()
    {
        Instantiate(ballPrefab, transform.position, Quaternion.identity);
    }
}
