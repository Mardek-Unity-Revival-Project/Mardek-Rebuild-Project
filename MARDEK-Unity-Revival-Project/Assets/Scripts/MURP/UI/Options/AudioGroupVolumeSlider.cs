using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioGroupVolumeSlider : MonoBehaviour
{
    [SerializeField] AudioGroupVolume audioGroupVolume;
    [SerializeField] Text groupNameText = null;
    [SerializeField] Slider volumeSlider = null;

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
        
        volumeSlider.value = audioGroupVolume.VolumeLinearValue;
    }
    private void UpdateVolumeText()
    {
        groupNameText.text = $"{audioGroupVolume.VolumeDisplayName} ({(volumeSlider.value * 100):0}%)";
    }

    public void ChangeVolume(float value)
    {
        audioGroupVolume.VolumeLinearValue = value;
        audioGroupVolume.SaveVolume();
        UpdateUI();
    }
}
