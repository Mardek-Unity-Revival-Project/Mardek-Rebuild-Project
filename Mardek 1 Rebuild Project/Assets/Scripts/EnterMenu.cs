using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnterMenu : MonoBehaviour
{
    bool open = false; //Menu starts closed
    private GameObject images;
    private GameObject buttons;

    void Start()
    {
        images = GameObject.Find("Images");
        buttons = GameObject.Find("Buttons"); 

        images.SetActive(false); //Disables the menu at the start
        buttons.SetActive(false); // ^

        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) //When enter is pressed
        {
            if (!open) //If menu is not open
            {
                images.SetActive(true); //Enables menu
                buttons.SetActive(true); // ^
                
                GameProgressData.lockdown = true; // Pauses the game
                open = true; //Menu is open
            }
            else
            {
                images.SetActive(false); // Disables menu
                buttons.SetActive(false); // ^
                GameProgressData.lockdown = false; // Resumes the game
                open = false; //Menu is closed
            }            
        }
    }
}
