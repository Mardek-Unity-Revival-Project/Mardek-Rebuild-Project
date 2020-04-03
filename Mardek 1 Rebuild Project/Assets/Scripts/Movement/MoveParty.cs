using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveParty : MonoBehaviour
{
    private float speed = 0.16f;
    private Rigidbody2D rb;
    private int waiting = Constants.movement_speed; //how long (frames) moving takes
    private int waiting_s;
    private Vector2 velocity;
    public Vector2 position;
    private MoveMardek Mardek;
    private MoveParty ahead;
    private Tagger tagger_N;
    private Tagger tagger_S;
    private Tagger tagger_W;
    private Tagger tagger_E;
    private int starter;
    private string nombre;
    private GameMaster GameMaster;
    private Sprite Sprite_up1;
    private Sprite Sprite_up2;
    private Sprite Sprite_down1;
    private Sprite Sprite_down2;//no need to drag! Just file nombre with charater's name!
    private Sprite Sprite_left1;
    private Sprite Sprite_left2;
    private Sprite Sprite_right1;
    private Sprite Sprite_right2;

    public SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        Renderer rend = GetComponent<Renderer>();
        rb = GetComponent<Rigidbody2D>();
        speed = speed / waiting;
        waiting_s = waiting;
        position = new Vector2(GameProgressData.x, GameProgressData.y);
        rb.MovePosition(position);
        GameObject go = GameObject.Find("PC 1");
        Mardek = go.GetComponent<MoveMardek>();
        starter = 0;
        if (gameObject.name == "PC 2")
        {
            starter = waiting_s;
            nombre = GameProgressData.party[1];
        }
        else if (gameObject.name == "PC 3")
        {
            go = GameObject.Find("PC 2");
            ahead = go.GetComponent<MoveParty>();
            starter = waiting_s;
            nombre = GameProgressData.party[2];

        }
        else if (gameObject.name == "PC 4")
        {
            go = GameObject.Find("PC 3");
            ahead = go.GetComponent<MoveParty>();
            starter = waiting_s * 3;
            nombre = GameProgressData.party[3];

        }
        else
            nombre = "";

        go = GameObject.Find("Tagger_W");
        tagger_W = go.GetComponent<Tagger>();
        go = GameObject.Find("Tagger_E");
        tagger_E = go.GetComponent<Tagger>();
        go = GameObject.Find("Tagger_N");
        tagger_N = go.GetComponent<Tagger>();
        go = GameObject.Find("Tagger_S");
        tagger_S = go.GetComponent<Tagger>();
        go = GameObject.Find("GameMaster");
        GameMaster = go.GetComponent<GameMaster>();
        if (nombre == "")
        {
            rend.enabled = false;
        }
        spriteRenderer = GetComponent<SpriteRenderer>(); // we are accessing the SpriteRenderer that is attached to the Gameobject
        Sprite_up1 = (Sprite)UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/Visual Assets/Sprites/Characters/" + nombre + "_Up1.png", typeof(Sprite));
        Sprite_up2 = (Sprite)UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/Visual Assets/Sprites/Characters/" + nombre + "_Up2.png", typeof(Sprite));
        Sprite_right1 = (Sprite)UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/Visual Assets/Sprites/Characters/" + nombre + "_Right1.png", typeof(Sprite));
        Sprite_right2 = (Sprite)UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/Visual Assets/Sprites/Characters/" + nombre + "_Right2.png", typeof(Sprite));
        Sprite_left1 = (Sprite)UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/Visual Assets/Sprites/Characters/" + nombre + "_Left1.png", typeof(Sprite));
        Sprite_left2 = (Sprite)UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/Visual Assets/Sprites/Characters/" + nombre + "_Left2.png", typeof(Sprite));
        Sprite_down1 = (Sprite)UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/Visual Assets/Sprites/Characters/" + nombre + "_Down1.png", typeof(Sprite));
        Sprite_down2 = (Sprite)UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/Visual Assets/Sprites/Characters/" + nombre + "_Down2.png", typeof(Sprite));
        spriteRenderer.sprite = Sprite_up1; // set the sprite to sprite1
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
            }

            return;
        }
        WalkAnim2();
        if (GameMaster.GetMovement() == Constants.DOWN)
        {
            if (tagger_S.free == true)
            {
                SetVelocity();
            }
            else
            {
                velocity = new Vector2(0, 0);
                return;
            }
        }
        else if (GameMaster.GetMovement() == Constants.UP)
        {
            if (tagger_N.free == true)
            {
                SetVelocity();
            }
            else
            {
                velocity = new Vector2(0, 0);
                return;
            }
        }
        else if (GameMaster.GetMovement() == Constants.RIGHT)
        {
            if (tagger_E.free == true)
            {
                SetVelocity();
            }
            else
            {
                velocity = new Vector2(0, 0);
                return;
            }
        }

        else if (GameMaster.GetMovement() == Constants.LEFT)
        {
            if (tagger_W.free == true)
            {
                SetVelocity();
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
        if (gameObject.name != "Main Camera")
        {
            if (velocity == new Vector2(0, speed))
            {
                spriteRenderer.sprite = Sprite_up1;
            }
            else if (velocity == new Vector2(0, -speed))
            {
                spriteRenderer.sprite = Sprite_down1;
            }
            else if (velocity == new Vector2(speed, 0))
            {
                spriteRenderer.sprite = Sprite_right1;
            }
            else if (velocity == new Vector2(-speed, 0))
            {
                spriteRenderer.sprite = Sprite_left1;
            }
        }
        waiting = 0;
        return;

    }
    void SetVelocity()
    {
        if (gameObject.name == "PC 2")
        {
            velocity = (Mardek.position - position) / waiting_s;
        }
        else
        {
            velocity = (ahead.position - position) / waiting_s;
        }
    }
    void WalkAnim1()
    {
        if (velocity != Vector2.zero && gameObject.name != "Main Camera")
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
        if (velocity != Vector2.zero && gameObject.name != "Main Camera")
        {
            if (spriteRenderer.sprite == Sprite_up2)
            {
                spriteRenderer.sprite = Sprite_up1;
            }
            else if (spriteRenderer.sprite == Sprite_down2)
            {
                spriteRenderer.sprite = Sprite_down1;
            }
            else if (spriteRenderer.sprite == Sprite_left2)
            {
                spriteRenderer.sprite = Sprite_left1;
            }
            else if (spriteRenderer.sprite == Sprite_right2)
            {
                spriteRenderer.sprite = Sprite_right1;
            }
        }
    }
}
