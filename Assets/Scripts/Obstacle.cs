using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    // Start is called before the first frame update

    public float rotationSpeed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate(){
        transform.Rotate(0, 0, rotationSpeed);
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player"){
            Destroy(other.gameObject);

            GameManager.instance.GameOver();
        }

        if (other.tag == "Ground"){
            GameManager.instance.IncreaseScore();
            Destroy(gameObject);
        }


    }
}
