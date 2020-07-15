using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "New Button", menuName = "MenuButton")]
public class MenuButton : ScriptableObject
{
    public Sprite normalSprite;
    public Sprite selectedSprite;

    public TMP_ColorGradient normalGradient;
    public TMP_ColorGradient selectedGradient;
}
