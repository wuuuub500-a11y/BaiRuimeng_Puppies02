// Assets/Scripts/GameStartBtn.cs
using UnityEngine;

public class GameStartBtn : MonoBehaviour
{
    [Header("Canvas Switch")]
    public GameObject startCanvas;
    public GameObject ingameCanvas;

    public void StartGame()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            gameManager.StartGame();
        }

        if (startCanvas != null)
        {
            startCanvas.SetActive(false);
        }

        if (ingameCanvas != null)
        {
            ingameCanvas.SetActive(true);
        }

        gameObject.SetActive(false);
    }
}