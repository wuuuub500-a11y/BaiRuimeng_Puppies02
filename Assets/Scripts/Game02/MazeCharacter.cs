using System.Collections;
using UnityEngine;

public class MazeCharacter : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 2.5f;

    // 默认方向改为“向下”
    [SerializeField] private Vector2 startDirection = Vector2.down;

    [Header("Collision / Wall")]
    [SerializeField] private float castDistance = 0.2f;

    [Tooltip("Any wall that blocks movement (normal + edge).")]
    [SerializeField] private LayerMask blockingWallLayers;

    [Tooltip("Walls that can be destroyed (normal walls only).")]
    [SerializeField] private LayerMask breakableWallLayers;

    [Header("Break Wall Rules")]
    [SerializeField] private float breakAfterSeconds = 5f;
    [SerializeField] private float stunSeconds = 2f;

    [Header("Animation")]
    [SerializeField] private Animator animator;

    private static readonly int IsDizzyHash = Animator.StringToHash("isDizzy");

    private Vector2 _dir;
    private float _turnTimer;
    private bool _isStunned;

    private void Awake()
    {
        if (animator == null)
            animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _dir = NormalizeCardinal(startDirection);
        _turnTimer = 0f;

        ApplyFacingRotation(_dir);
        SetDizzy(false);
    }

    private void Update()
    {
        if (_isStunned)
            return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            TryTurnRight();
        }

        TickTurnTimer();

        // Move if not blocked by any wall (normal + edge). Otherwise keep "bumping" the wall.
        if (!IsBlocked(_dir, out _))
        {
            transform.position += (Vector3)(_dir * (moveSpeed * Time.deltaTime));
        }
        else
        {
            TryBreakWallIfReady();
        }
    }

    private void TryTurnRight()
    {
        _turnTimer = 0f;
        _dir = RotateRight(_dir);

        // 移动方向旋转时，物体也跟着旋转
        ApplyFacingRotation(_dir);
    }

    private void TickTurnTimer()
    {
        _turnTimer += Time.deltaTime;
    }

    private void TryBreakWallIfReady()
    {
        if (_turnTimer < breakAfterSeconds)
            return;

        // Only break walls on the "breakable" layer(s).
        if (!IsBreakableBlocked(_dir, out var hit))
            return;

        Destroy(hit.collider.gameObject);
        StartCoroutine(StunCoroutine());
    }

    private IEnumerator StunCoroutine()
    {
        _isStunned = true;
        SetDizzy(true);

        yield return new WaitForSeconds(stunSeconds);

        _isStunned = false;
        SetDizzy(false);

        // Reset bump/break timer after stun.
        _turnTimer = 0f;
    }

    private void SetDizzy(bool value)
    {
        if (animator != null)
            animator.SetBool(IsDizzyHash, value);
    }

    private bool IsBlocked(Vector2 dir, out RaycastHit2D hit)
    {
        var origin = (Vector2)transform.position;
        hit = Physics2D.Raycast(origin, dir, castDistance, blockingWallLayers);
        return hit.collider != null;
    }

    private bool IsBreakableBlocked(Vector2 dir, out RaycastHit2D hit)
    {
        var origin = (Vector2)transform.position;
        hit = Physics2D.Raycast(origin, dir, castDistance, breakableWallLayers);
        return hit.collider != null;
    }

    private static Vector2 RotateRight(Vector2 dir)
    {
        if (dir == Vector2.up) return Vector2.right;
        if (dir == Vector2.right) return Vector2.down;
        if (dir == Vector2.down) return Vector2.left;
        return Vector2.up;
    }

    private static Vector2 NormalizeCardinal(Vector2 dir)
    {
        if (Mathf.Abs(dir.x) >= Mathf.Abs(dir.y))
            return dir.x >= 0 ? Vector2.right : Vector2.left;

        return dir.y >= 0 ? Vector2.up : Vector2.down;
    }

    private void ApplyFacingRotation(Vector2 dir)
    {
        // 约定：开始向下移动时 rotation 为 (0,0,0)
        float z;
        if (dir == Vector2.up) z = 180f;
        else if (dir == Vector2.right) z = 90f;
        else if (dir == Vector2.down) z = 0f;
        else z = -90f; // left

        transform.rotation = Quaternion.Euler(0f, 0f, z);
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        var o = (Vector2)transform.position;
        var d = Application.isPlaying ? _dir : NormalizeCardinal(startDirection);
        Gizmos.DrawLine(o, o + d * castDistance);
    }
#endif
}