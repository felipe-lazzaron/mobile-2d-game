using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    public Animator animator;
    public float moveSpeed;
    SpriteRenderer sp;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
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
}
