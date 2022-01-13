using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MURP.UI
{
    public class FramerateCapOption : MonoBehaviour
    {
        const string framerateCapPlayerPrefsKey = "framerateCap";
        [SerializeField] Text frameRateCapValueLabel = null;
        [SerializeField] Slider slider = null;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void LoadValue()
        {
            var value = PlayerPrefs.GetInt(framerateCapPlayerPrefsKey);
            if (value == default)
                value = Application.targetFrameRate;
            SetCapValue(value);
        }
        static void SetCapValue(int value)
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = value;
            PlayerPrefs.SetInt(framerateCapPlayerPrefsKey, value);
        }

        void Start()
        {
            var value = Application.targetFrameRate;
            slider.value = value;
            UpdateLabel(value);
        }
        void UpdateLabel(int value)
        {
            frameRateCapValueLabel.text = value.ToString();
        }
        public void OnSliderChanged(float value)
        {
            var intValue = Mathf.RoundToInt(value);
            SetCapValue(intValue);
            UpdateLabel(intValue);
        }
    }
}