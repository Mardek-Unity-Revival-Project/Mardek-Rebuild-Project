using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AudioObject : ScriptableObject
{
    [SerializeField] protected AudioClip clip = default;

    public abstract void PlayOnSource(AudioSource audioSource);
}
