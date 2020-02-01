using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tagger : MonoBehaviour
{

    public bool free = false;
    public Rigidbody2D rb;
    private GameObject GameObject;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Impassable")
        {
            free = false;
            GameObject=collision.gameObject;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Impassable" && GameObject == collision.gameObject)
        {
            free = true;
        }
    }
}
