using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomStarter : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "PC 1")
        {
            GameObject go = GameObject.Find("GameMaster");
            GameMaster gameMaster=go.GetComponent<GameMaster>();
            gameMaster.setcustom();
        }
        string str = collision.gameObject.name;
        string str2 = collision.gameObject.name;

    }
}
