using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;

    Rigidbody2D rb;
    GameManager gameManager;
    Animator animator;

    
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(gameManager.gameStarted == false)
        {
            return;
        }
        
        float move = Input.GetAxis("Horizontal");

        transform.Translate(Vector3.right * move * moveSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("jump");
        }
    }
}