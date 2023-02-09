using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{

    public static SoundManager instance;
    [SerializeField]
    private AudioSource soundFX;
    [SerializeField]
    private AudioClip landClip, deathClip, iceBreakClip, gameOverClip;
    public Sprite mute;
    public Sprite unmute;

    public Image ButtonImg;

    void Awake(){
        if(PlayerPrefs.GetInt("Sound") == 0){
            Mute();
        }else{
            UnMute();
        }
        if(instance == null){
            instance = this;
        }
    }

    public void landSound(){
        soundFX.clip = landClip;
        soundFX.Play();
    }

    public void iceBreakSound(){
        soundFX.clip = iceBreakClip;
        soundFX.Play();
    }

    public void DeathSound(){
        soundFX.clip = deathClip;
        soundFX.Play();
    }

    public void gameOverSound(){
        soundFX.clip = gameOverClip;
        soundFX.Play();
    }
    public void Mute(){
        GetComponent<AudioSource>().mute = true;
        soundFX.mute = true;
        PlayerPrefs.SetInt("Sound", 0);
        ButtonImg.sprite = mute;
    }
    public void UnMute(){
        GetComponent<AudioSource>().mute = false;
        soundFX.mute = false;
        PlayerPrefs.SetInt("Sound", 1);
        ButtonImg.sprite = unmute;
    }
    public void soundButtonClick(){
        if(PlayerPrefs.GetInt("Sound") == 1){
            Mute();
        }else{
            UnMute();
        }
    }
}
