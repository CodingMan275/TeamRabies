using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SaveData/Audio")]
public class AudioData : ScriptableObject
{
    public float Master = .8f;
    public float Vocal = .8f;
    public float SFX = .8f;

}
