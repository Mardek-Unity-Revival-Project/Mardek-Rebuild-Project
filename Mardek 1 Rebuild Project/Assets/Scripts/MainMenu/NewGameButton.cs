using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NewGameButton : MonoBehaviour
{
    public TextMeshProUGUI myText;
    public Color oldColor;
    public Color newColor;
    public Sprite newButtonSprite;
    public Sprite oldButtonSprite;
    public Image buttonImage;

    public GameObject GameNameInput;

    // Start is called before the first frame update
    void Start()
    {
        GameNameInput.SetActive(false);
    }

    private void OnMouseDown()
    {
        GameNameInput.SetActive(true);
    }

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
