using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartBtn : MonoBehaviour
{
    public GameObject dog1;
    public GameObject dog2;
    public void StartGame()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            gameManager.gameStarted = true;
        }
        dog1.SetActive(false);
        dog2.SetActive(true);
        gameObject.SetActive(false);
        
    }
}
