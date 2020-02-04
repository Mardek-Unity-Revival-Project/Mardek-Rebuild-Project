using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnterMenu : MonoBehaviour
{
    private Image image;
    bool open = false; //Menu starts closed
    
    // Update is called once per frame
    void Update()
    {

        GameObject go = GameObject.Find("EnterMenuBackground"); //Finds EnterMenuBackground image
        image = go.GetComponent<Image>(); // Stores the image component in variable

        if (Input.GetKeyDown(KeyCode.KeypadEnter)) //When enter is pressed
        {
            if (!open) //If menu is not open
            {
                image.enabled = true; //Open menu
                open = true; //Menu is open
            }
            else
            {
                image.enabled = false; //Closes menu
                open = false; //Menu is closed
            }
            
        }
    }
}
