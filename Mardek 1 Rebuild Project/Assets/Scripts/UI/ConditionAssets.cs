using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Assets", menuName = "ConditionAssets")]
public class ConditionAssets : ScriptableObject
{
    public Sprite healthBarGreen;
    public Sprite healthBarBrown;
    public Sprite healthBarRed;

    public Sprite manaBarBlue;
    public Sprite xpBarYellow;

    public Color healthColorGreen;
    public Color UnderlayColorGreen;
    public Color healthColorBrown;
    public Color UnderlayColorBrown;
    public Color healthColorRed;
    public Color UnderlayColorRed;
    public Color manaColorBlue;
    public Color UnderlayColorBlue;
    public Color xpColorYellow;
}
