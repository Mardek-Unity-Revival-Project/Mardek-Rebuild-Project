using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FullscreenButton : MonoBehaviour
{
    [SerializeField] Toggle toggle;
    bool skipFrame = false;

    private void Update()
    {
        if (skipFrame)
            skipFrame = false;
        else
            toggle.isOn = Screen.fullScreen;
    }

    public void OnValueChanged(bool value)
    {
        Screen.fullScreen = value;
        skipFrame = true;
    }
}