using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    private Slider slider;
    private ParticleSystem particleSys;
    private GameManager gameManager;

    public float FillSpeed = 0.5f;
    private float targetProgress = 0;

    private void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
        particleSys = GameObject.Find("ProgressBarParticles")?.GetComponent<ParticleSystem>();
        if (particleSys == null)
        {
            Debug.LogError("ProgressBarParticles not found or missing ParticleSystem component.");
        }
    }

    void Start()
    {
        gameManager = GameManager.instance;
        if (gameManager == null)
        {
            Debug.LogError("GameManager instance not found.");
        }
        SetProgress(0.0f);
    }
    // Update is called once per frame
    void Update()
    {
        if (slider.value < targetProgress)
        {
            slider.value += FillSpeed * Time.deltaTime;
            if (!particleSys.isPlaying)
                particleSys.Play();
        }
        // else if (!gameManager.IsInfoPanelActive())
        // {
        //     particleSys.Stop();
        //     if (slider.value >= 1f)
        //     {
        //         if (gameManager != null)
        //         {
        //             gameManager.OnProgressInfo();
        //         }
        //     }
        // }
    }

    public void SetProgress(float progress)
    {
        targetProgress = progress;
    }
    public void ResetProgress()
    {
        slider.value = 0;
        targetProgress = 0;  // Garante que o target também comece de 0
    }
}
