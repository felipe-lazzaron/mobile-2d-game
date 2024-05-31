using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingObstacle : MonoBehaviour
{
    private GameObject player;
    private float speed = 4.5f;

    public float rotationSpeed;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
    }

    void Update()
    {
        if (player != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
        
    }
    private void FixedUpdate(){
        transform.Rotate(0, 0, rotationSpeed);
    }

    private void OnTriggerEnter2D(Collider2D other){
        if (other.tag == "Player" && !GameManager.instance.playerController.isInvincible)
        {
            GameManager.instance.GameOver();
        }

        if (other.tag == "Ground"){
            GameManager.instance.IncreaseScore();
            Destroy(gameObject);
        }


    }

}
