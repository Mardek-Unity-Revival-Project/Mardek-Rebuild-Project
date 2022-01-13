using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MURP.UI
{ 
    public class ResolutionOptions : MonoBehaviour
    {
        [SerializeField] Dropdown dropdown = null;
        List<Resolution> resolutions = new List<Resolution>();
        //[SerializeField] Toggle fullscreen = null;

        private void OnEnable()
        {
            PopulateDropdown();
        }

        void PopulateDropdown()
        {
            List<Dropdown.OptionData> resolutionOptions = new List<Dropdown.OptionData>();
            ListResolutionsInDescendingOrder(ref resolutionOptions, ref resolutions);
            dropdown.options = resolutionOptions;
            UpdateDropdownValueWithCurrentResolution(resolutions);
        }

        void UpdateDropdownValueWithCurrentResolution(List<Resolution> resolutions)
        {
            for (int i = 0; i < resolutions.Count; i++)
            {
                bool resolutionMatchWithCurrentResolution =
                    resolutions[i].height == Screen.height
                    && resolutions[i].width == Screen.width;
                if (resolutionMatchWithCurrentResolution)
                {
                    dropdown.value = i;
                    break;
                }
            }   
        }

        void ListResolutionsInDescendingOrder(ref List<Dropdown.OptionData> options, ref List<Resolution> resolutionList)
        {
            resolutionList = new List<Resolution>();
            options = new List<Dropdown.OptionData>();
            for (int i = Screen.resolutions.Length - 1; i >= 0; i--)
            {
                var resolution = Screen.resolutions[i];
                var resolutionString = $"{resolution.width}x{resolution.height}";
                var optionData = new Dropdown.OptionData(resolutionString, null);
                options.Add(optionData);
                resolutionList.Add(resolution);
            }
        }

        public void ApplyNewResolution(int value)
        {
            var lastIndex = Screen.resolutions.Length - 1;
            var desiredResolution = Screen.resolutions[lastIndex - value];
            UpdateResolution(desiredResolution);
        }

        void UpdateResolution(Resolution desiredResolution)
        {
            Screen.SetResolution(desiredResolution.width, desiredResolution.height, Screen.fullScreen);
        }
    }
}