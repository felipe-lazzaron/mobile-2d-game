using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    bool gameOver = false;

    int score = 0;

    public TextMeshProUGUI scoreText;

    private void Awake()
    {
            instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver(){
        gameOver = true;

        GameObject.Find("ObstacleSpawner").GetComponent<ObstacleSpawner>().StopSpawning();
    }

    public void IncreaseScore(){
        if(!gameOver){
            score++;    
            scoreText.text = score.ToString();
            print(score);   
        }
            
    }
}
