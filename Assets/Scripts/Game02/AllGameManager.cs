using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AllGameManager : MonoBehaviour
{
    public static AllGameManager Instance; // 单例模式，方便其他脚本直接调用

    [Header("--- UI 组件 ---")]
    public TextMeshProUGUI timerText; // 拖入你的 TimerText (如果是旧版Text，类型改为 public Text timerText;)

    private float timeRemaining = 60f;
    private bool isGameActive = false;
    private bool isGameOver = false;

    void Awake()
    {
        // 初始化单例
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // 该方法由选狗按钮的 On Click() 触发调用
    public void StartGameCountdown()
    {
        timeRemaining = 60f;
        isGameActive = true;
        isGameOver = false;
        UpdateTimerUI();
    }

    void Update()
    {
        if (!isGameActive || isGameOver) return;

        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerUI();
        }
        else
        {
            timeRemaining = 0;
            UpdateTimerUI();
            LevelFailed(); // 时间耗尽，触发失败
        }
    }

    void UpdateTimerUI()
    {
        if (timerText != null)
        {
            // Mathf.CeilToInt 可以让倒计时从 60 优雅地数到 0，不会出现小数点
            timerText.text = Mathf.CeilToInt(timeRemaining).ToString() + "s";
        }
    }

    // 成功判定
    public void LevelSuccess()
    {
        if (isGameOver) return;
        isGameOver = true;
        isGameActive = false;

        Debug.Log("【判定成功】小狗在60s内成功到达终点！");
        // TODO: 在这里写你希望展示的成功界面、暂停游戏或者播放获胜音效
    }

    // 失败判定
    void LevelFailed()
    {
        if (isGameOver) return;
        isGameOver = true;
        isGameActive = false;

        Debug.Log("【判定失败】60s时间到，未能到达终点！");
        // TODO: 在这里写你希望展示的失败界面（如弹出 Restart 按钮等）
    }
}
