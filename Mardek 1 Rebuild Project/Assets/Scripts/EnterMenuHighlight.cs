using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class EnterMenuHighlight : MonoBehaviour, ISelectHandler
{
    public GameObject targetButton;
    public Image targetImage;
    public Sprite selectedSprite;
    public Sprite normalSprite;
    public TextMeshProUGUI text;
    public TMP_ColorGradient selectedGradient;
    public TMP_ColorGradient originalGradient;



    public void OnSelect(BaseEventData eventData)
    {
        targetImage.sprite = selectedSprite;
        text.colorGradientPreset = selectedGradient;
    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject != targetButton)
        {
            targetImage.sprite = normalSprite;
            text.colorGradientPreset = originalGradient;
        }
    }
}
