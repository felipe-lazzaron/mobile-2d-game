using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimation : MonoBehaviour
{
    AudioManager audioManager;
    public Animator animator;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }


    public void PlayGame(){
        animator.SetTrigger("Play");
        audioManager.PlaySFX(audioManager.playButton);
    }
}
