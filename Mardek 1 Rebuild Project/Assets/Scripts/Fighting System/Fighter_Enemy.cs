using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter_Enemy : MonoBehaviour
{
    public Rigidbody2D rb;
    public CombatMaster combatMaster;
    public SpriteRenderer spriteRenderer;
    public Sprite Sprite;
    public Fighter fighter;


    public void Create(Fighter _fighter)
    {
        fighter = _fighter;
        rb = GetComponent<Rigidbody2D>();
        GameObject go = GameObject.Find("Combat Master");
        combatMaster = go.GetComponent<CombatMaster>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Sprite = (Sprite)UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/Visual Assets/Sprites/Characters/" + fighter.sprite + "_Right1.png", typeof(Sprite));

        if (fighter.alive)
            spriteRenderer.sprite = Sprite;
        else
            spriteRenderer.sprite = null;
    }


}
