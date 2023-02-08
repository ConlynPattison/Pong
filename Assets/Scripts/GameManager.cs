using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    public GameObject p1ScoreZone;
    public GameObject p2ScoreZone;
    
    public void PlayerWon()
    {
        p1ScoreZone.GetComponent<ScoreZone>().Reset();
        p2ScoreZone.GetComponent<ScoreZone>().Reset();
    }
}
