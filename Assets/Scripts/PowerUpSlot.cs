using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PowerUpSlot : MonoBehaviour
{
    public Image slotIcon;
    public Button useButton;
    public PowerUp currentPowerUp;
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }


    private void Start()
    {
        useButton.onClick.AddListener(UsePowerUp);
        ClearSlot();
    }

    public void AssignPowerUp(PowerUp powerUp)
    {
        print("deu powerup");
        currentPowerUp = powerUp;
        slotIcon.sprite = powerUp.icon;
        slotIcon.enabled = true;
        useButton.interactable = true;
    }

    public void ClearSlot()
    {
        slotIcon.enabled = false;  // Desativa o ícone no slot
        useButton.interactable = false;  // Torna o botão inativo
        currentPowerUp = null;  // Limpa o power-up atual
    }

    public void UsePowerUp()
    {
        Debug.Log("UsePowerUp method called.");
        if (currentPowerUp != null)
        {
            Debug.Log("Current PowerUp: " + currentPowerUp);
            switch (currentPowerUp.powerUpName)
            {
                case "Shield":
                    StartCoroutine(ActivateInvincibility(currentPowerUp.duration));
                    break;
                case "SpeedUp":
                    StartCoroutine(ActivateSpeedBoost(currentPowerUp.duration));
                    break;
                // Implementar outros casos
            }
            ClearSlot();
        }
        else
        {
            Debug.Log("No current PowerUp assigned.");
        }
    }
    IEnumerator ActivateInvincibility(float duration)
    {
        Debug.Log("Invencibilidade ativada");
        GameManager.instance.playerController.isInvincible = true;

        yield return new WaitForSeconds(duration);

        audioManager.PlaySFX(audioManager.playPowerDown);
        GameManager.instance.playerController.isInvincible = false;
        Debug.Log("Invencibilidade desativada");
    }

    IEnumerator ActivateSpeedBoost(float duration)
    {
        Debug.Log("Aumento de velocidade ativado");
        PlayerController player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        if (player != null)
        {
            player.moveSpeed = player.moveSpeed * 2.5f;  // Supondo que a velocidade normal seja multiplicada por 2
        }

        yield return new WaitForSeconds(duration);  // Aguarda a duração definida

        if (player != null)
        {
            audioManager.PlaySFX(audioManager.playPowerDown);
            player.moveSpeed = 5.0f;  // Retorna a velocidade ao normal
        }
        Debug.Log("Aumento de velocidade desativado");
    }
}
