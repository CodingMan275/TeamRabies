using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeSettings : MonoBehaviour
{
    public AudioMixer Mixer;
    public Slider Master;
    public Slider SFX;
    public Slider Vocals;

    public AudioData AD;

    private void Awake()
    {
        Master.onValueChanged.AddListener(MasterVolume);
        SFX.onValueChanged.AddListener(SFXVolume);
        Vocals.onValueChanged.AddListener(VocalVolume);
    }
    public void SetVolume()
    {
        Master.value = AD.Master;
        SFX.value = AD.SFX;
        Vocals.value = AD.Vocal;
        Mixer.SetFloat("Master",Mathf.Log10(AD.Master) * 20);
        Mixer.SetFloat("SFX",Mathf.Log10(AD.SFX) * 20);
        Mixer.SetFloat("Vocal",Mathf.Log10(AD.Vocal) * 20);

    }
    

    void MasterVolume(float f)
    {
        AD.Master = f;
        Mixer.SetFloat("Master", Mathf.Log10(f) * 20);
    }
    void SFXVolume(float f)
    {
        AD.SFX = f;
        Mixer.SetFloat("SFX", Mathf.Log10(f) * 20);
    }
    void VocalVolume(float f)
    {
        AD.Vocal = f;
        Mixer.SetFloat("Vocal", Mathf.Log10(f) * 20);
    }

    

    public void Mute(bool m)
    {
        Mixer.SetFloat("Master", Mathf.Log10( m ? 0.0001f:AD.Master ) * 20);
    }
}
