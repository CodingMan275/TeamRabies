using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System;

public class VolumeSettings : MonoBehaviour
{
    public AudioMixer Mixer;
    public Slider Master;
    public Slider SFX;
    public Slider Vocals;
    public Slider Music;

    public AudioData AD;

    private void Awake()
    {
        Master.onValueChanged.AddListener(MasterVolume);
        SFX.onValueChanged.AddListener(SFXVolume);
        Vocals.onValueChanged.AddListener(VocalVolume);
        Music.onValueChanged.AddListener(MusicVolume);
    }
    public void SetVolume()
    {
        Master.value = AD.Master;
        SFX.value = AD.SFX;
        Vocals.value = AD.Vocal;
        Music.value = AD.Music;
        Mixer.SetFloat("Master",Mathf.Log10(AD.Master) * 20);
        Mixer.SetFloat("SFX",Mathf.Log10(AD.SFX) * 20);
        Mixer.SetFloat("Vocal",Mathf.Log10(AD.Vocal) * 20);
        Mixer.SetFloat("Music",Mathf.Log10(AD.Music) * 20);

    }
    

    void MasterVolume(float f)
    {
        AD.Master = (float)Math.Round(f, 1);
        Mixer.SetFloat("Master", Mathf.Log10(f) * 20);
    }
    void SFXVolume(float f)
    {
        AD.SFX = (float)Math.Round(f, 1);
        Mixer.SetFloat("SFX", Mathf.Log10(f) * 20);
    }
    void VocalVolume(float f)
    {
        AD.Vocal = (float)Math.Round(f, 1);
        Mixer.SetFloat("Vocal", Mathf.Log10(f) * 20);
    }
    void MusicVolume(float f)
    {
        AD.Music = (float)Math.Round(f, 1);
        Mixer.SetFloat("Music", Mathf.Log10(f) * 20);
    }


    public void Mute(bool m)
    {
        Mixer.SetFloat("SFX", Mathf.Log10( m ? 0.0001f:AD.SFX ) * 20);
        Mixer.SetFloat("Vocal", Mathf.Log10( m ? 0.0001f:AD.Vocal ) * 20);
    }
}
