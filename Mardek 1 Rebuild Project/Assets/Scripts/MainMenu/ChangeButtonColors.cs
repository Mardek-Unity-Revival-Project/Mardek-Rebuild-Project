using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


// Class for making the buttons and their text change color on mouse hover.
public class ChangeButtonColors : MonoBehaviour
{
    public TextMeshProUGUI myText;
    public Color oldColor;
    public Color newColor;
    public Sprite newButtonSprite;
    public Sprite oldButtonSprite;
    public Image buttonImage;


    void OnMouseOver()
    {
        myText.color = newColor;
        buttonImage.sprite = newButtonSprite;
    }

    void OnMouseExit()
    {
        
        myText.color = oldColor;
        buttonImage.sprite = oldButtonSprite;
        
    }
}
