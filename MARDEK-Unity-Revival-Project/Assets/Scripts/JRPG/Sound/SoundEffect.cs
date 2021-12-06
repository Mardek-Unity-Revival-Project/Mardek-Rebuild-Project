using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Audio/SoundEffect")]
public class SoundEffect : AudioObject
{
    public override void PlayOnSource(AudioSource audioSource)
    {
        audioSource.PlayOneShot(clip);
    }
}
