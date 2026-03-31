// Assets/Scripts/GameManager.cs
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public bool gameStarted;
    public GameObject frisbee;

    [Header("UI (TextMeshPro)")]
    public TMP_Text timerText;
    public TMP_Text scoreText;

    [Header("Canvas Switch")]
    public GameObject ingameCanvas;
    public GameObject endCanvas;

    [Header("End UI")]
    public Image endResultImage;
    public Sprite winSprite;
    public Sprite loseSprite;

    [Header("Game Rules")]
    public float gameDuration = 60f;
    public int winScoreThreshold = 7;

    [Header("Spawn")]
    public float spawnInterval = 2f;

    private float currentTime;
    private float spawnTimer;
    private int score;
    private bool gameEnded;

    private void Start()
    {
        ResetGameState();
        ApplyCanvasState();
        UpdateUI();
    }

    private void Update()
    {
        if (!gameStarted || gameEnded)
        {
            return;
        }

        currentTime -= Time.deltaTime;
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0f)
        {
            SpawnFrisbee();
            spawnTimer = spawnInterval;
        }

        if (currentTime <= 0f)
        {
            currentTime = 0f;
            EndGame();
        }

        UpdateUI();
    }

    public void StartGame()
    {
        ResetGameState();
        gameStarted = true;
        ApplyCanvasState();
        UpdateUI();
    }

    public void AddScore(int amount = 1)
    {
        if (gameEnded)
        {
            return;
        }

        score += amount;
        UpdateUI();
    }

    public void SpawnFrisbee()
    {
        if (frisbee == null)
        {
            return;
        }

        float randomX = Random.Range(-6f, 6f);
        Instantiate(frisbee, new Vector3(randomX, 0f, 0f), Quaternion.identity);
    }

    private void EndGame()
    {
        gameEnded = true;
        gameStarted = false;

        // 规则：结束时 \> 7 分获胜，\<= 7 分失败
        bool win = score > winScoreThreshold;

        if (endResultImage != null)
        {
            endResultImage.sprite = win ? winSprite : loseSprite;
            endResultImage.enabled = endResultImage.sprite != null;
        }

        ApplyCanvasState();
        UpdateUI();
    }

    private void ResetGameState()
    {
        currentTime = gameDuration;
        spawnTimer = spawnInterval;
        score = 0;
        gameEnded = false;
        gameStarted = false;
    }

    private void ApplyCanvasState()
    {
        if (ingameCanvas != null)
        {
            ingameCanvas.SetActive(gameStarted && !gameEnded);
        }

        if (endCanvas != null)
        {
            endCanvas.SetActive(gameEnded);
        }
    }

    private void UpdateUI()
    {
        if (timerText != null)
        {
            timerText.text = $"Time: {Mathf.CeilToInt(currentTime)}";
        }

        if (scoreText != null)
        {
            scoreText.text = $"Score: {score}";
        }
    }
}
