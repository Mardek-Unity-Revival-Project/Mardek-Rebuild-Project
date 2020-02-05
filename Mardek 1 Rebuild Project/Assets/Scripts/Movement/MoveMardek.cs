using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveMardek : MonoBehaviour
{
    private float speed = 0.16f; //how many pixels /100 I move.
    private Rigidbody2D rb;
    private int waiting = Constants.movement_speed; //how long (frames) moving takes
    private int waiting_s;
    public Vector2 velocity;
    public Vector2 position;
    private Tagger tagger;
    private Tagger tagger_N;
    private Tagger tagger_S;
    private Tagger tagger_W;
    private Tagger tagger_E;
    private GameMaster GameMaster;
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
        speed = speed / waiting;
        waiting_s = waiting;
        position = new Vector2(ApplicationData.x, ApplicationData.y);
        rb.MovePosition(position);
        GameObject go = GameObject.Find("Tagger");
        go.tag = "Tagger";
        tagger = go.GetComponent<Tagger>();
        go = Instantiate(go);
        go.tag = "Untagged";
        go.name = "Tagger_E";
        tagger_E = go.GetComponent<Tagger>();
        go = Instantiate(go);
        go.name = "Tagger_W";
        tagger_W = go.GetComponent<Tagger>();
        go = Instantiate(go);
        go.name = "Tagger_S";
        tagger_S = go.GetComponent<Tagger>();
        go = Instantiate(go);
        go.name = "Tagger_N";
        tagger_N = go.GetComponent<Tagger>();
        go= GameObject.Find("GameMaster");
        GameMaster = go.GetComponent<GameMaster>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // we are accessing the SpriteRenderer that is attached to the Gameobject
        spriteRenderer.sprite = Sprite_up1; // set the sprite to sprite1
        velocity = Vector2.zero;
        MoveTaggers();
    }

    void Update() // Is called every frame
    {
        if (waiting < waiting_s)
        {
            waiting++;
            position = position + velocity;
            rb.MovePosition(position);
            if (waiting == waiting_s / 2)
            {
                WalkAnim1();
                GameMaster.Move();
            }

            return;
        }
        WalkAnim2();
        if (GameMaster.GetMovement()==Constants.DOWN)
        {
            tagger.rb.MovePosition(position + new Vector2(0, -0.16f));
            spriteRenderer.sprite = Sprite_down1;
            if (tagger_S.free == true)
            {
                velocity = new Vector2(0, -speed);
                MoveTaggers();
            }
            else
            {
                velocity = new Vector2(0, 0);
                return;
            }
        }
        else if (GameMaster.GetMovement() == Constants.UP)
        {
            tagger.rb.MovePosition(position + new Vector2(0, 0.16f));
            spriteRenderer.sprite = Sprite_up1;
            if (tagger_N.free == true)
            {
                velocity = new Vector2(0, speed);
                MoveTaggers();
            }
            else
            {
                velocity = new Vector2(0, 0);
                return;
            }
        }
        else if (GameMaster.GetMovement() == Constants.RIGHT)
        {
            tagger.rb.MovePosition(position + new Vector2(0.16f, 0));
            spriteRenderer.sprite = Sprite_right1;
            if (tagger_E.free == true)
            {
                velocity = new Vector2(speed, 0);
                MoveTaggers();
            }
            else
            {
                velocity = new Vector2(0, 0);
                return;
            }
        }

        else if (GameMaster.GetMovement() == Constants.LEFT)
        {
            tagger.rb.MovePosition(position + new Vector2(-0.16f, 0));
            spriteRenderer.sprite = Sprite_left1;
            if (tagger_W.free == true)
            {
                velocity = new Vector2(-speed, 0);
                MoveTaggers();
            }
            else
            {
                velocity = new Vector2(0, 0);
                return;
            }
        }
        else
        {
            velocity = new Vector2(0, 0);
            return;
        }
        waiting = 0;
        return;

    }
    void MoveTaggers()
    {
        tagger_W.rb.MovePosition(position + velocity * waiting_s + new Vector2(-0.16F, 0));
        tagger_E.rb.MovePosition(position + velocity * waiting_s + new Vector2(0.16f, 0));
        tagger_S.rb.MovePosition(position + velocity * waiting_s + new Vector2(0, -0.16f));
        tagger_N.rb.MovePosition(position + velocity * waiting_s + new Vector2(0, 0.16f));
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
