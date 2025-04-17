using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SaveData/Audio")]
public class AudioData : ScriptableObject
{
    [Range(0.0001f , 1f)]public float Master = .8f;
    [Range(0.0001f, 1f)] public float Vocal = .8f;
    [Range(0.0001f, 1f)] public float SFX = .8f;
    [Range(0.0001f, 1f)] public float Music = .8f;

    public void Reset()
    {
        Master = .8f;
        Vocal = .8f;
        SFX = .8f;
        Music = .8f;
    }

}
