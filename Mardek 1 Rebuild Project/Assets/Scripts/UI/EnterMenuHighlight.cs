using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;


public class EnterMenuHighlight : MonoBehaviour, ISelectHandler
{
    private GameObject targetButton;
    private Image targetImage;
    private TextMeshProUGUI text;
    private GameObject tabTextGo;
    public MenuButton menuButton;
    private List<Button> menuButtonList;


    public void Start()
    {

        targetButton = gameObject;
        targetImage = gameObject.GetComponentInChildren<Image>();
        tabTextGo = GameObject.Find("TabText");
        text = gameObject.GetComponentInChildren<TextMeshProUGUI>();

        menuButtonList = new List<Button>()
    {
        GameObject.Find("PartyButton").GetComponent<Button>(), GameObject.Find("SkillsButton").GetComponent<Button>(),
        GameObject.Find("InventoryButton").GetComponent<Button>(), GameObject.Find("MapButton").GetComponent<Button>(),
        GameObject.Find("QuestsButton").GetComponent<Button>(), GameObject.Find("PlotItemsButton").GetComponent<Button>(),
        GameObject.Find("StatusButton").GetComponent<Button>(), GameObject.Find("MedalsButton").GetComponent<Button>(),
        GameObject.Find("EncyclopaediaButton").GetComponent<Button>(), GameObject.Find("OptionsButton").GetComponent<Button>(),
        GameObject.Find("HelpButton").GetComponent<Button>()
    };
}

    // Highlights the selected button
    public void OnSelect(BaseEventData eventData)
    {
        targetImage.sprite = menuButton.selectedSprite;
        text.colorGradientPreset = menuButton.selectedGradient;
    }

    // Update is called once per frame
    void Update()
    {
        // Resets sprites once something else is selected
        if (EventSystem.current.currentSelectedGameObject != targetButton)
        {
            targetImage.sprite = menuButton.normalSprite;
            text.colorGradientPreset = menuButton.normalGradient;
        }

        // Updates text in upper left corner (tabtext)
        TextMeshProUGUI tabText = tabTextGo.GetComponent<TextMeshProUGUI>();
        if (menuButtonList.Contains(EventSystem.current.currentSelectedGameObject.GetComponent<Button>()))
        {
            tabText.text = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<TextMeshProUGUI>().text;
        }
    }
}
