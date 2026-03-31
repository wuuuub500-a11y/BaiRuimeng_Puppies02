// Assets/Scripts/DogController.cs
using UnityEngine;

public class DogController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;

    [Header("Catch FX")]
    public GameObject catchFxPrefab;
    public GameObject missFxPrefab;
    public Transform fxSpawnPoint;

    private Rigidbody2D rb;
    private GameManager gameManager;
    private Animator animator;

    public int score = 0;
    private bool isOverlappingFrisbee = false;
    private GameObject frisbee;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (gameManager == null || gameManager.gameStarted == false)
        {
            return;
        }

        float move = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * move * moveSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("jump");

            if (isOverlappingFrisbee)
            {
                if (frisbee != null)
                {
                    Destroy(frisbee.transform.parent != null ? frisbee.transform.parent.gameObject : frisbee);
                }

                animator.SetBool("success", true);
                gameManager.AddScore(1);
                SpawnFx(catchFxPrefab);

                Invoke(nameof(SetAnimationFalse), 1f);
            }
            else
            {
                SpawnFx(missFxPrefab);
            }
        }
    }

    private void SpawnFx(GameObject prefab)
    {
        if (prefab == null) return;

        Transform spawn = fxSpawnPoint != null ? fxSpawnPoint : transform;
        Instantiate(prefab, spawn.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Frisbee"))
        {
            isOverlappingFrisbee = true;
            frisbee = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Frisbee"))
        {
            isOverlappingFrisbee = false;
            if (frisbee == other.gameObject)
            {
                frisbee = null;
            }
        }
    }

    private void SetAnimationFalse()
    {
        animator.SetBool("success", false);
    }
}
