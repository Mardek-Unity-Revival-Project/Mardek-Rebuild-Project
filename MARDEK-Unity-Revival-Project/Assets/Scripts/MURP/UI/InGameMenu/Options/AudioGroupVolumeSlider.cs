using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioGroupVolumeSlider : MonoBehaviour
{
    [SerializeField] AudioMixerGroup audioGroup = null;
    [SerializeField] Text groupNameText = null;
    [SerializeField] Slider volumeSlider = null;

    string volumeString { get { return $"{audioGroup.name} Volume"; } }

    private void OnValidate()
    {
        UpdateVolumeText();
    }
    private void OnEnable()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        UpdateSliderValue();
        UpdateVolumeText();
    }
    private void UpdateSliderValue()
    {
        audioGroup.audioMixer.GetFloat(volumeString, out var value);
        value = Mathf.Pow(10f, value / 20f);
        volumeSlider.value = value;
    }
    private void UpdateVolumeText()
    {
        groupNameText.text = $"{volumeString} ({(volumeSlider.value * 100):0}%)";
    }

    public void ChangeVolume(float value)
    {
        value = Mathf.Clamp(value, 0.0001f, value); // avoid log10 of 0
        var newVolume = Mathf.Log10(value) * 20;
        audioGroup.audioMixer.SetFloat(volumeString, newVolume);
        UpdateUI();
    }
}
