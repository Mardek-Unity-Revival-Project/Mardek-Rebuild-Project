using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class NewGame : MonoBehaviour
{
    public TMP_InputField inputField;
    public string inputString;

    void OnMouseDown()
    {
        inputString = inputField.text;  //Takes the user input for save game naming.

        GameProgressData.party[0]="Mardek_Hero";
        GameProgressData.party[1] = "Deugan_Hero";
        GameProgressData.x = 0.08f;
        GameProgressData.y = -0.4f;
        SceneManager.LoadScene("DL_Area1");
        
    }
}
