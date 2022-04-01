using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class AudioGroupVolume
{
    [SerializeField] AudioMixerGroup audioGroup = null;
    string PlayerPrefKey
    {
        get
        {
            return audioGroup.name;
        }
    }
    public string VolumeDisplayName
    {
        get 
        { 
            return $"{audioGroup.name} Volume"; 
        } 
    }
    public float VolumeLinearValue
    {
        get
        {
            audioGroup.audioMixer.GetFloat(VolumeDisplayName, out var value);
            value = Mathf.Pow(10f, value / 20f);
            return value;
        }
        set
        {
            var linearVolume = Mathf.Clamp(value, 0.0001f, 1f); // avoid log10 of 0
            var dbVolume = Mathf.Log10(linearVolume) * 20;
            audioGroup.audioMixer.SetFloat(VolumeDisplayName, dbVolume);
        }
    }
    public void SaveVolume()
    {
        PlayerPrefs.SetFloat(PlayerPrefKey, VolumeLinearValue);
    }
    public void LoadVolume()
    {
        var value = PlayerPrefs.GetFloat(PlayerPrefKey, -1f);
        if(value != -1f)
        {
            VolumeLinearValue = value;
        }
    }
}