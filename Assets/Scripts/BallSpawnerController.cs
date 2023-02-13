using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawnerController : MonoBehaviour
{
    public GameObject ballPrefab;
    // Start is called before the first frame update
    void Start()
    {
        NextServe();
    }

    public void NextServe()
    {
        Instantiate(ballPrefab, transform.position, Quaternion.identity);
    }
}
