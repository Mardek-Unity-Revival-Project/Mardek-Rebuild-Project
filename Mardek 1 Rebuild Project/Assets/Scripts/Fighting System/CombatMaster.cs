using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatMaster : MonoBehaviour
{
    Fighter_Enemy[] left = new Fighter_Enemy[4];
    Fighter_Player[] right= new Fighter_Player[4];


    // Update is called once per frame
    void Start()
    {
        //remove this
        GameFile.e[0] = new Fighter("Evil Mardek", "Mardek_Hero", 20, 25, 25, 17, 23, 24, 1, 40, 0, 0, 35);
        GameFile.e[1] = new Fighter("Evil Deugan", "Deugan_Hero", 25, 23, 30, 25, 19, 19, 1, 40, 0, 0, 35);
        GameFile.e[2] = new Fighter();
        GameFile.e[3] = new Fighter();
        //remove this

        string str;
        for (int i = 0; i < 4; i++)
        {
            str = "e" + i.ToString();
            GameObject go = GameObject.Find(str);
            left[i]= go.GetComponent<Fighter_Enemy>();
            left[i].Create(GameFile.e[i]);
        }
    }
}
