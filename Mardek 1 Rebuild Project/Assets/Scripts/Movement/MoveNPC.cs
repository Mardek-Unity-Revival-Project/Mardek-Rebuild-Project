using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveNPC : MonoBehaviour
{
    // public Constants constants = new Constants(); - this is (perheps) unnecessary?
    private float speed = 0.16f; //how many pixels /100 I move.
    private Rigidbody2D rb;
    public int Speed; //how long (frames) moving takes
    public int delay = 0;
    private int delay_s;
    private int waiting_s;
    private Vector2 velocity;
    private Vector2 position;
    public GameObject Object;
    private Tagger tagger;
    private int movement;
    public string nombre;
    private Sprite Sprite_up1;
    private Sprite Sprite_up2;
    private Sprite Sprite_down1;
    private Sprite Sprite_down2;//drag sprites here
    private Sprite Sprite_left1;
    private Sprite Sprite_left2;
    private Sprite Sprite_right1;
    private Sprite Sprite_right2;
    public SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        Sprite_up1 = (Sprite)UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/Visual Assets/Sprites/Characters/" + nombre + "_Up1.png", typeof(Sprite));
        Sprite_up2 = (Sprite)UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/Visual Assets/Sprites/Characters/" + nombre + "_Up2.png", typeof(Sprite));
        Sprite_right1 = (Sprite)UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/Visual Assets/Sprites/Characters/" + nombre + "_Right1.png", typeof(Sprite));
        Sprite_right2 = (Sprite)UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/Visual Assets/Sprites/Characters/" + nombre + "_Right2.png", typeof(Sprite));
        Sprite_left1 = (Sprite)UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/Visual Assets/Sprites/Characters/" + nombre + "_Left1.png", typeof(Sprite));
        Sprite_left2 = (Sprite)UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/Visual Assets/Sprites/Characters/" + nombre + "_Left2.png", typeof(Sprite));
        Sprite_down1 = (Sprite)UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/Visual Assets/Sprites/Characters/" + nombre + "_Down1.png", typeof(Sprite));
        Sprite_down2 = (Sprite)UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/Visual Assets/Sprites/Characters/" + nombre + "_Down2.png", typeof(Sprite));
        rb = GetComponent<Rigidbody2D>();
        speed = speed / Speed;
        waiting_s = Speed;
        delay_s = delay;
        position = rb.position; // for same reason, using rb.position insteed of position breaks the code.
        GameObject go = GameObject.Find("Tagger");
        Object= Instantiate(go);
        tagger = Object.GetComponent<Tagger>();
    }

    void Update() // Is called every frame
    {
        if (Speed < waiting_s)
        {
            if (Speed == -1)
            {
                Speed++;
                return;
            }
            if (tagger.free == false && Speed == 0)
            {
                velocity = Vector2.zero;
            }
            Speed++;
            position = position + velocity;
            rb.MovePosition(position);
            return;
        }
        if (delay<delay_s)
        {
                delay++;
                return;
        }
        movement = Random.Range(1, 5);
        if (movement == Constants.UP) //go up
            {
                velocity = new Vector2(0, speed);
                tagger.rb.MovePosition(position + new Vector2(0, 0.16f));
            }
            else if (movement == Constants.DOWN) // go down
            {
                velocity = new Vector2(0, -speed);
                tagger.rb.MovePosition(position + new Vector2(0, -0.16f));

            }
            else if (movement == Constants.RIGHT) //go right
            {
                velocity = new Vector2(speed, 0);
                tagger.rb.MovePosition(position + new Vector2(0.16f, 0));

            }

            else if (movement == Constants.LEFT) // go left
            {
                velocity = new Vector2(-speed, 0);
                tagger.rb.MovePosition(position + new Vector2(-0.16f, 0));
            }
            else
            {
            velocity = Vector2.zero;
            return;
            }
        Speed = -1;
        delay = 0;
        return;

    }
    void WalkAnim1()
    {
        if (velocity != Vector2.zero)
        {
            if (spriteRenderer.sprite == Sprite_up1)
            {
                spriteRenderer.sprite = Sprite_up2;
            }
            else if (spriteRenderer.sprite == Sprite_down1)
            {
                spriteRenderer.sprite = Sprite_down2;
            }
            else if (spriteRenderer.sprite == Sprite_left1)
            {
                spriteRenderer.sprite = Sprite_left2;
            }
            else if (spriteRenderer.sprite == Sprite_right1)
            {
                spriteRenderer.sprite = Sprite_right2;
            }
        }

    }
    void WalkAnim2()
    {
        if (velocity != Vector2.zero)
        {
            if (spriteRenderer.sprite == Sprite_up2)
            {
                spriteRenderer.sprite = Sprite_up1;
                tagger.rb.MovePosition(position + new Vector2(0, 0.16f));
            }
            else if (spriteRenderer.sprite == Sprite_down2)
            {
                spriteRenderer.sprite = Sprite_down1;
                tagger.rb.MovePosition(position + new Vector2(0, -0.16f));
            }
            else if (spriteRenderer.sprite == Sprite_left2)
            {
                spriteRenderer.sprite = Sprite_left1;
                tagger.rb.MovePosition(position + new Vector2(-0.16f, 0));
            }
            else if (spriteRenderer.sprite == Sprite_right2)
            {
                spriteRenderer.sprite = Sprite_right1;
                tagger.rb.MovePosition(position + new Vector2(0.16f, 0));
            }
        }
    }
}
