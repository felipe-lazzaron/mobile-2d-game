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
    bool gamePaused = false;
    Vector3 playerStartPos;

    int score = 0;

    public TextMeshProUGUI scoreText;
    public GameObject gameOverPanel;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        instance = this;
    }

    private void Start()
    {
        audioManager.PlayBackgroundMusic(audioManager.backgroundMusicGame);
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerStartPos = player.transform.position; // Save the starting position of the player
        }
    }

    public void GameOver()
    {
        gameOver = true;
        audioManager.StopBackgroundMusic();
        audioManager.PlaySFX(audioManager.loseSound);

        GameObject.Find("ObstacleSpawner").GetComponent<ObstacleSpawner>().StopSpawning();

        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            PlayerController playerController = player.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.Die(); // Mark player as dead
            }
        }

        gameOverPanel.SetActive(true);
    }

    public void IncreaseScore()
    {
        if (!gameOver)
        {
            score++;
            scoreText.text = score.ToString();
            print(score);
        }
    }

    public void Restart()
    {
        audioManager.PlaySFX(audioManager.playButton);
        StartCoroutine(LoadSceneAfterSound(audioManager.playButton.length, "Game"));
    }

    public void Menu()
    {
        audioManager.PlaySFX(audioManager.playButton);
        StartCoroutine(LoadSceneAfterSound(audioManager.playButton.length, "MainMenu"));
    }

    public void PauseGame()
    {
        gamePaused = true;
        audioManager.StopBackgroundMusic();
        GameObject.Find("ObstacleSpawner").GetComponent<ObstacleSpawner>().StopSpawning();

        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            PlayerController playerController = player.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.enabled = false;
                playerController.rb.velocity = Vector2.zero;
                playerController.animator.SetBool("isMoving", false);
            }
        }
    }

    public void ResumeGame()
    {
        gamePaused = false;
        gameOver = false;
        audioManager.PlayBackgroundMusic(audioManager.backgroundMusicGame);
        GameObject.Find("ObstacleSpawner").GetComponent<ObstacleSpawner>().StartSpawning();

        // Reative e reinicialize o jogador
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            player.SetActive(true);
            player.transform.position = playerStartPos; // Restore player's starting position
            SpriteRenderer playerRenderer = player.GetComponent<SpriteRenderer>();
            if (playerRenderer != null)
            {
                playerRenderer.enabled = true;
            }

            // Reinicialize o PlayerController se necessário
            PlayerController playerController = player.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.Revive();
                playerController.enabled = true;
                playerController.rb.velocity = Vector2.zero;
                playerController.animator.SetBool("isMoving", false);
            }
        }

        gameOverPanel.SetActive(false);
    }

    private IEnumerator LoadSceneAfterSound(float delay, string sceneName)
    {
        yield return new WaitForSecondsRealtime(delay); // Use WaitForSecondsRealtime to handle time scale
        SceneManager.LoadScene(sceneName);
    }
}
