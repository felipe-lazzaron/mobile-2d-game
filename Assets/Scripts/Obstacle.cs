using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float rotationSpeed;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        transform.Rotate(0, 0, rotationSpeed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            GameManager.instance.PauseGame();
            GameManager.instance.GameOver();
        }

        if (other.tag == "Ground")
        {
            GameManager.instance.IncreaseScore();
            Destroy(gameObject);
        }
    }
}
