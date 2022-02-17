using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FullscreenButton : MonoBehaviour
{
    [SerializeField] Toggle toggle;

    private void OnEnable()
    {
        toggle.isOn = Screen.fullScreen;
    }

    public void OnValueChanged(bool value)
    {
        Screen.fullScreen = value;
        StartCoroutine(UpdateAfter1Frame());
    }

    IEnumerator UpdateAfter1Frame()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        toggle.isOn = Screen.fullScreen;
        yield return null;
    }
}