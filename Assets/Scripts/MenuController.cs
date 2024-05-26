using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }


    public void Quit()
    {
        audioManager.PlaySFX(audioManager.playButton);
        StartCoroutine(LoadSceneAfterSound(audioManager.playButton.length));
        
    }

    private IEnumerator LoadSceneAfterSound(float delay)
    {       
        yield return new WaitForSeconds(delay);
        Application.Quit();
    }


}
