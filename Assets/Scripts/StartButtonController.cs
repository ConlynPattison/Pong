using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StartButtonController : MonoBehaviour
{
    public GameObject gameManager;

    public void OnButtonClick()
    {
        gameManager.GetComponent<GameManager>().StartButtonCall();
    }
}
