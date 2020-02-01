using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourSpriteAnimator : MonoBehaviour
{
    public Sprite Sprite_1; // Drag your first sprite here
    public Sprite Sprite_2; // Drag your second sprite here
    public Sprite Sprite_3;
    public Sprite Sprite_4;
    int timer = 0;
    public int timerInput;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // we are accessing the SpriteRenderer that is attached to the Gameobject
        if (spriteRenderer.sprite == null) // if the sprite on spriteRenderer is null then
            spriteRenderer.sprite = Sprite_1; // set the sprite to sprite1
    }

    void Update()
    {
        timer++;

        if (timer == timerInput) //Edit this to change the animation speed
        {
            ChangeTheDamnSprite(); //changes the damn sprite
            timer = 0;
        }

    }

    void ChangeTheDamnSprite()
    {
        if (spriteRenderer.sprite == Sprite_1) // if the spriteRenderer sprite = sprite1 then change to sprite2
        {
            spriteRenderer.sprite = Sprite_2;
        }
        else if (spriteRenderer.sprite == Sprite_2)
        {
            spriteRenderer.sprite = Sprite_3;
        }
        else if (spriteRenderer.sprite == Sprite_3)
        {
            spriteRenderer.sprite = Sprite_4;
        }
        else if (spriteRenderer.sprite == Sprite_4)
        {
            spriteRenderer.sprite = Sprite_1;
        }
    }
}
