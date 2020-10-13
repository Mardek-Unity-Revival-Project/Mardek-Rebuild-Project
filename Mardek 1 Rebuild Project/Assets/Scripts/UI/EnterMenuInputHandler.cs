using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnterMenuInputHandler : MonoBehaviour
{
    public MenuButton MenuButton;
    public Button[] buttonList;
    private int activeButtonIndex = 0;
    public TextMeshProUGUI tabName;

    public float timeToWaitUntilFastScrolling = 0.5f;
    public float scrollSpeed = 0.1f;
    private float cooldown = 0f;
    

    // Start is called before the first frame update
    void Start()
    {
        tabName = GameObject.Find("TabText").GetComponent<TextMeshProUGUI>();
        activeButtonIndex = 0;

        buttonList[activeButtonIndex].GetComponentInChildren<Image>().sprite = MenuButton.selectedSprite;
        buttonList[activeButtonIndex].GetComponentInChildren<TextMeshProUGUI>().colorGradientPreset = MenuButton.selectedGradient;
        tabName.text = buttonList[activeButtonIndex].name;
    }

    // Update is called once per frame
    void Update()
    {
        cooldown -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MenuMovement(1);
            cooldown = timeToWaitUntilFastScrolling;
        }
        if (Input.GetKey(KeyCode.DownArrow) && cooldown < 0)
        {
            MenuMovement(1);
            cooldown = scrollSpeed;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MenuMovement(-1);
            cooldown = timeToWaitUntilFastScrolling;
        }
        if (Input.GetKey(KeyCode.UpArrow) && cooldown < 0)
        {
            MenuMovement(-1);
            cooldown = scrollSpeed;
        }
    }

    void MenuMovement(int direction)
    {
        buttonList[activeButtonIndex].GetComponentInChildren<Image>().sprite = MenuButton.normalSprite;
        buttonList[activeButtonIndex].GetComponentInChildren<TextMeshProUGUI>().colorGradientPreset = MenuButton.normalGradient;
        activeButtonIndex += direction;

        if (activeButtonIndex >= buttonList.Length)
            activeButtonIndex = 0;

        if (activeButtonIndex < 0)
            activeButtonIndex = buttonList.Length - 1;

        buttonList[activeButtonIndex].GetComponentInChildren<Image>().sprite = MenuButton.selectedSprite;
        buttonList[activeButtonIndex].GetComponentInChildren<TextMeshProUGUI>().colorGradientPreset = MenuButton.selectedGradient;
        tabName.text = buttonList[activeButtonIndex].name;
    }
}
