using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadAudioGroups : MonoBehaviour
{
    [SerializeField] List<AudioGroupVolume> audioGroups = new List<AudioGroupVolume>();

    private void Start()
    {
        foreach (var group in audioGroups)
            group.LoadVolume();
    }
}