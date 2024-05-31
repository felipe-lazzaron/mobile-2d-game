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

    public PlayerController playerController;

    public ProgressBar progressBar;
    public GameObject infoPanel;
    private bool infoPanelActive = false;

    int score = 0;

    public TextMeshProUGUI scoreText;
    public GameObject gameOverPanel;
    public Button closeButton; // Adicione uma referência ao botão de fechar

    private int pointsToNextPowerUp = 10;
    public PowerUp[] powerUps; // Array de todos os power-ups
    public PowerUpSlot powerUpSlot; // Referência ao slot de power-up

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
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
        progressBar = GameObject.Find("ProgressBar")?.GetComponent<ProgressBar>();
        if (progressBar == null)
        {
            Debug.LogError("ProgressBar not found or missing ProgressBar component.");
        }
        if (infoPanel != null)
        {
            closeButton = infoPanel.GetComponentInChildren<Button>();
            if (closeButton != null)
            {
                closeButton.onClick.AddListener(CloseInfoPanel); // Adicione o listener ao botão de fechar
            }
            else
            {
                Debug.LogError("Close button not found in infoPanel.");
            }
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
            UpdateProgressBar();
            if (score % pointsToNextPowerUp == 0)
            {
                audioManager.PlaySFX(audioManager.playPowerUp);
                progressBar.ResetProgress();
                AssignRandomPowerUp();
            }
            GameObject.Find("ObstacleSpawner").GetComponent<ObstacleSpawner>().UpdateSpawnRate(score);
        }
    }
    void AssignRandomPowerUp()
    {
        int index = UnityEngine.Random.Range(0, powerUps.Length); // Escolhe um power-up aleatório
        powerUpSlot.AssignPowerUp(powerUps[index]);
        
    }

    private void UpdateProgressBar()
    {

        float progress = (float)score / pointsToNextPowerUp;
        float resetableProgress = progress - (int)progress;
        progressBar.SetProgress(resetableProgress);


    }


    public void OnProgressInfo()
    {
        // Pausa o jogo
        Time.timeScale = 0;
        infoPanelActive = true;
        if (infoPanel != null)
        {
            infoPanel.SetActive(true); // Ativa o painel de conclusão
        }
        else
        {
            Debug.LogError("InfoPanel is null in OnProgressInfo.");
        }
    }

    public void CloseInfoPanel()
    {
        Debug.Log("Close Panel");
        // Desativa o painel de conclusão
        if (infoPanel != null)
        {
            infoPanel.SetActive(false); // Desativa o painel de conclusão
            infoPanelActive = false;
        }
        else
        {
            Debug.LogError("InfoPanel is null in CloseInfoPanel.");
        }
        // Retoma o jogo
        Time.timeScale = 1;
    }

    public bool IsInfoPanelActive()
    {
        return infoPanelActive;
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
