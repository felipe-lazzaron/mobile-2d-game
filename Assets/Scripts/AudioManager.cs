using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("Audio Clip")]
    public AudioClip playButton;
    public AudioClip playPowerUp;
    public AudioClip playPowerDown;
    public AudioClip backgroundMusicMenu;
    public AudioClip backgroundMusicGame;
    public AudioClip loseSound;

    private void Start(){
        musicSource.clip = backgroundMusicMenu;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip){
        SFXSource.PlayOneShot(clip);
    }

    public void StopBackgroundMusic(){
        musicSource.Stop();
    }

    public void PlayBackgroundMusic(AudioClip clip){
        musicSource.clip = clip;
        musicSource.Play();
    }
}
