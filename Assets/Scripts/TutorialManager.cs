using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] tutorialImages; // Array para armazenar as imagens do tutorial
    private int currentImageIndex = 0;

    private void Start()
    {
        // Redefine os PlayerPrefs para teste inicial
        // Apenas para garantir que o tutorial apareça novamente durante o desenvolvimento
        PlayerPrefs.DeleteAll();

        // Verifica se é a primeira vez que o jogo é executado
        Debug.Log("FirstRun value: " + PlayerPrefs.GetInt("FirstRun", 1));

        if (PlayerPrefs.GetInt("FirstRun", 1) == 1)
        {
            ShowTutorial();
            PlayerPrefs.SetInt("FirstRun", 0);
            PlayerPrefs.Save();
        }
        else
        {
            EndTutorial();
        }
    }

    private void ShowTutorial()
    {
        Debug.Log("Mostrando tutorial...");
        foreach (GameObject image in tutorialImages)
        {
            image.SetActive(false);
        }
        tutorialImages[currentImageIndex].SetActive(true);
    }

    public void NextImage()
    {
        Debug.Log("Avançando para a próxima imagem do tutorial...");
        tutorialImages[currentImageIndex].SetActive(false);
        currentImageIndex++;

        if (currentImageIndex < tutorialImages.Length)
        {
            tutorialImages[currentImageIndex].SetActive(true);
        }
        else
        {
            EndTutorial();
        }
    }

    private void EndTutorial()
    {
        Debug.Log("Terminando tutorial...");
        foreach (GameObject image in tutorialImages)
        {
            image.SetActive(false);
        }
        // Lógica adicional para iniciar o jogo ou mostrar o menu principal
    }
}
