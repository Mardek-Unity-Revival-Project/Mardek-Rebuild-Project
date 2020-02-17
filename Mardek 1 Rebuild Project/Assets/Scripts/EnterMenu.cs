using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnterMenu : MonoBehaviour
{
    private Image image;
    private TextMeshProUGUI text;
    private TextMeshProUGUI text2;
    private TextMeshProUGUI text3;
    private TextMeshProUGUI text4;
    private TextMeshProUGUI text5;
    private TextMeshProUGUI text6;
    private TextMeshProUGUI text7;
    private TextMeshProUGUI text8;
    private TextMeshProUGUI text9;
    private TextMeshProUGUI text10;
    private TextMeshProUGUI text11;
    bool open = false; //Menu starts closed
    
    // Update is called once per frame
    void Update()
    {

        GameObject go = GameObject.Find("EnterMenuBackground"); //Finds EnterMenuBackground gameobject
        image = go.GetComponent<Image>(); // Stores the image component in variable

        GameObject go2 = GameObject.Find("PartyButton");
        GameObject go3 = GameObject.Find("SkillsButton");
        GameObject go4 = GameObject.Find("InventoryButton");
        GameObject go5 = GameObject.Find("MapButton");
        GameObject go6 = GameObject.Find("QuestsButton");
        GameObject go7 = GameObject.Find("PlotItemsButton");
        GameObject go8 = GameObject.Find("StatusButton");
        GameObject go9 = GameObject.Find("MedalsButton");
        GameObject go10 = GameObject.Find("EncyclopaediaButton");
        GameObject go11 = GameObject.Find("OptionsButton");
        GameObject go12 = GameObject.Find("HelpButton");
        
        text = go2.GetComponent<TextMeshProUGUI>();
        text2 = go3.GetComponent<TextMeshProUGUI>();
        text3 = go4.GetComponent<TextMeshProUGUI>();
        text4 = go5.GetComponent<TextMeshProUGUI>();
        text5 = go6.GetComponent<TextMeshProUGUI>();
        text6 = go7.GetComponent<TextMeshProUGUI>();
        text7 = go8.GetComponent<TextMeshProUGUI>();
        text8 = go9.GetComponent<TextMeshProUGUI>();
        text9 = go10.GetComponent<TextMeshProUGUI>();
        text10 = go11.GetComponent<TextMeshProUGUI>();
        text11 = go12.GetComponent<TextMeshProUGUI>();




        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) //When enter is pressed
        {
            if (!open) //If menu is not open
            {
                image.enabled = true; //Open menu
                text.enabled = true;
                text2.enabled = true;
                text3.enabled = true;
                text4.enabled = true;
                text5.enabled = true;
                text6.enabled = true;
                text7.enabled = true;
                text8.enabled = true;
                text9.enabled = true;
                text10.enabled = true;
                text11.enabled = true;
                ApplicationData.lockdown = true;
                open = true; //Menu is open
            }
            else
            {
                image.enabled = false; //Closes menu
                text.enabled = false;
                text2.enabled = false;
                text3.enabled = false;
                text4.enabled = false;
                text5.enabled = false;
                text6.enabled = false;
                text7.enabled = false;
                text8.enabled = false;
                text9.enabled = false;
                text10.enabled = false;
                text11.enabled = false;
                ApplicationData.lockdown = false;
                open = false; //Menu is closed
            }
            
        }
    }
}
