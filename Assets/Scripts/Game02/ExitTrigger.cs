using UnityEngine;

public class ExitTrigger : MonoBehaviour
{
    // 当有 2D 刚体物体进入带有 Is Trigger 的碰撞框时自动触发
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 严格匹配触碰物体的名字是否为 B-1_0 或 D-1_0
        if (collision.name == "B-1_0" || collision.name == "D-1_0")
        {
            // 找到主控制器，宣布胜利
            // 找到主控制器，宣布胜利
            if (AllGameManager.Instance != null)
            {
                AllGameManager.Instance.LevelSuccess();
            }
        }
    }
}