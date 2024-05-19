using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    AudioManager audioManager;
    public static GameManager instance;
    bool gameOver = false;

    int score = 0;

    public TextMeshProUGUI scoreText;
    public GameObject gameOverPanel;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        instance = this;
    }

    private void Start(){
        audioManager.PlayBackgroundMusic(audioManager.backgroundMusicGame);
    }

    public void GameOver(){
        gameOver = true;
        audioManager.StopBackgroundMusic();  // Para a música de fundo
        audioManager.PlaySFX(audioManager.loseSound);

        GameObject.Find("ObstacleSpawner").GetComponent<ObstacleSpawner>().StopSpawning();

        gameOverPanel.SetActive(true);
    }

    public void IncreaseScore(){
        if(!gameOver){
            score++;    
            scoreText.text = score.ToString();
            print(score);   
        }
    }

    public void Restart(){
        audioManager.PlaySFX(audioManager.playButton);
        StartCoroutine(LoadSceneAfterSound(audioManager.playButton.length, "Game"));
    }

    public void Menu(){
        audioManager.PlaySFX(audioManager.playButton);
        StartCoroutine(LoadSceneAfterSound(audioManager.playButton.length, "MainMenu"));
    }

    private IEnumerator LoadSceneAfterSound(float delay, string sceneName)
    {       
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);    }

}
