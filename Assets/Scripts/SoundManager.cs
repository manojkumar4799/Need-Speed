using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SoundManager : MonoBehaviour
{
    public SetSound[] setSound;
    public AudioSource BGM;
    public AudioSource SFX;
    private static SoundManager soundManager;
    public static SoundManager Instance()
    {
        return soundManager;
    }
    private void Start()
    {
        SoundManager.Instance().PlayBGM(Sound.introBGM);
    }
    private void Awake()
    {
        if (soundManager == null)
        {
            soundManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayBGM(Sound sound)
    {
        AudioClip audioClip;
        SetSound findSound = Array.Find(setSound, searchSound => searchSound.soundType == sound);
        if (findSound != null)
        {
            audioClip = findSound.clip;
            BGM.clip = audioClip;
            if (findSound.soundType == Sound.BGMSOund)
            {
                BGM.loop = true;
                BGM.volume = 0.65f;
            }
            else
            {
                BGM.loop = false;
                BGM.volume = 1f;
            }
            BGM.Play();
        }
        else return;
    }

    public void PlaySFX(Sound sound)
    {
        AudioClip audioClip;
        SetSound findSound = Array.Find(setSound, searchSound => searchSound.soundType == sound);
        if (findSound != null)
        {
            audioClip = findSound.clip;
            if (sound == Sound.missileExplosion)
            {
                SFX.volume = 0.4f;
            }
            else
            {
                SFX.volume = 1f;
            }
            SFX.PlayOneShot(audioClip);
        }
        else
        {
            return;
        }

    }
}
[Serializable]
public class SetSound
{
    public Sound soundType;
    public AudioClip clip;
}
public enum Sound
{
    BGMSOund,
    Explosion,
    enter,
    save, 
    Gameover,
    missileExplosion,
    introBGM,
    win
}
