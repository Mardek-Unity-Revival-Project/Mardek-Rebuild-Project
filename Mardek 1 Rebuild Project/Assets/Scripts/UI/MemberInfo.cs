using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MemberInfo : MonoBehaviour
{
    public ConditionAssets ConditionAssets;

    private Image memberImage;
    private TextMeshProUGUI memberName;
    private TextMeshProUGUI memberLevel;
    private TextMeshProUGUI memberTitle;
    private Image element;
    private Image healthBar;
    private TextMeshProUGUI healthNumber;
    private TextMeshProUGUI maxHealthNumber;
    private Image manaBar;
    private TextMeshProUGUI manaNumber;
    private TextMeshProUGUI maxManaNumber;
    private Image xpBar;
    private TextMeshProUGUI xpNumber;

    private const int healthBarWidth = 200;
    private const int healthBarHeight = 20;
    private const float healthBarInsent = (float)-205;

    private const int manaBarWidth = 160;
    private const float manaBarInsent = (float)20;

    private const int xpBarWidth = 110;
    private const float xpBarInsent = (float)200;

    private float health;
    private float maxHealth;


    void OnEnable()
    {

        memberName = transform.Find("Texts").Find("Name").GetComponent<TextMeshProUGUI>();
        memberLevel = transform.Find("Texts").Find("Lvl").GetComponent<TextMeshProUGUI>();
        memberTitle = transform.Find("Texts").Find("Title").GetComponent<TextMeshProUGUI>();
        element = transform.Find("Element").GetComponent<Image>();
        healthBar = transform.Find("HealthBar").GetComponent<Image>();
        healthNumber = transform.Find("Texts").Find("HPnumber").GetComponent<TextMeshProUGUI>();
        maxHealthNumber = transform.Find("Texts").Find("HPmaxnumber").GetComponent<TextMeshProUGUI>();
        manaBar = transform.Find("ManaBar").GetComponent<Image>();
        manaNumber = transform.Find("Texts").Find("MPnumber").GetComponent<TextMeshProUGUI>();
        maxManaNumber = transform.Find("Texts").Find("MPmaxnumber").GetComponent<TextMeshProUGUI>();
        xpBar = transform.Find("XpBar").GetComponent<Image>();
        xpNumber = transform.Find("Texts").Find("XPpercent").GetComponent<TextMeshProUGUI>();

        health = 100;
        maxHealth = 100;
        memberName.text = "Name";
        memberLevel.text = "Lvl:10";
        memberTitle.text = "Title";
        healthNumber.text = "100";
        maxHealthNumber.text = "100";


        // Healthbar control
        if (health < 0.3 * maxHealth)
        {
            healthBar.sprite = ConditionAssets.healthBarRed;
            healthNumber.color = ConditionAssets.healthColorRed;
            healthNumber.fontSharedMaterial.SetColor(ShaderUtilities.ID_UnderlayColor, ConditionAssets.UnderlayColorRed);
            maxHealthNumber.color = ConditionAssets.healthColorRed;
            maxHealthNumber.fontSharedMaterial.SetColor(ShaderUtilities.ID_UnderlayColor, ConditionAssets.UnderlayColorRed);
        }
        else if (health < 0.4 * maxHealth)
        {
            healthBar.sprite = ConditionAssets.healthBarBrown;
            healthNumber.color = ConditionAssets.healthColorBrown;
            healthNumber.fontSharedMaterial.SetColor(ShaderUtilities.ID_UnderlayColor, ConditionAssets.UnderlayColorBrown);
            maxHealthNumber.color = ConditionAssets.healthColorBrown;
            maxHealthNumber.fontSharedMaterial.SetColor(ShaderUtilities.ID_UnderlayColor, ConditionAssets.UnderlayColorBrown);
        }
        else
        {
            healthBar.sprite = ConditionAssets.healthBarGreen;
            healthNumber.color = ConditionAssets.healthColorGreen;
            healthNumber.fontSharedMaterial.SetColor(ShaderUtilities.ID_UnderlayColor, ConditionAssets.UnderlayColorGreen);
            maxHealthNumber.color = ConditionAssets.healthColorGreen;
            maxHealthNumber.fontSharedMaterial.SetColor(ShaderUtilities.ID_UnderlayColor, ConditionAssets.UnderlayColorGreen);
        }
        healthBar.rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, healthBarInsent, (health / maxHealth) * healthBarWidth);

        // Manabar control
        manaBar.rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, manaBarInsent, (float)0.5 * manaBarWidth);

        // Xpbar control
        xpBar.rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, xpBarInsent, (float)0.5 * xpBarWidth);

    }
}
