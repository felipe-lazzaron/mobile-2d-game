using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    public float moveSpeed;
    public SpriteRenderer sp;

    private bool isDead = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        Debug.Log("PlayerController started.");
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (isDead || Time.timeScale == 0) return; // Prevent movement when dead or game is paused

        if (Input.GetMouseButton(0))
        {
            if (Input.mousePosition.x < Screen.width / 2)
            {
                //move left
                rb.velocity = Vector2.left * moveSpeed;
                //flip the sprite
                sp.flipX = true;
                animator.SetBool("isMoving", true);
            }
            else
            {
                //move right
                rb.velocity = Vector2.right * moveSpeed;
                //flip the sprite
                sp.flipX = false;
                animator.SetBool("isMoving", true);
            }
        }
        else
        {
            //stop the player
            rb.velocity = Vector2.zero;
            animator.SetBool("isMoving", false);
        }
    }

    public void Die()
    {
        isDead = true;
        rb.velocity = Vector2.zero;
        animator.SetBool("isMoving", false);
    }

    public void Revive()
    {
        isDead = false;
    }
}
