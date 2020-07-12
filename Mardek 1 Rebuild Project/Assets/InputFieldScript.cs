using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// This class controls when the input field is shown
public class InputFieldScript : MonoBehaviour
{
    public TMP_InputField inputField;
    private string inputString;
    public GameObject beginButton;


    // At first it is hidden
    private void Start()
    {
        beginButton.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        inputString = inputField.text;
        if (inputString != "")
        {
            beginButton.SetActive(true);
        }
        else
        {
            beginButton.SetActive(false);
        }
    }
}
