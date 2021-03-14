using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnterMenu : MonoBehaviour
{
    bool open = false; //Menu starts closed
    public GameObject enterMenu;


    void Start()
    {
        enterMenu.SetActive(false); //Sets menus disabled at start   
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Return)) //When enter is pressed
        {
            if (!open) //If menu is not open
            {                
                GameProgressData.lockdown = true; // Pauses the game
                open = true; //Menu is open
            }
            else
            {                
                GameProgressData.lockdown = false; // Resumes the game
                open = false; //Menu is closed
            }
            enterMenu.SetActive(open);
        }
    }
}
