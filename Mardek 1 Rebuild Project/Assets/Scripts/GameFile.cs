using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameFile
{
    //party
    public static string[] party_members = new string[4] { "Mardek_Hero", "Deugan_Hero", "", "" };
    //position
    public static float x = 0.08f;
    public static float y = -0.4f;

    //custom scenario
    public static List<int> Custom_Played = new List<int>();
    public static bool lockdown;

    //Battle system
    public static Fighter[] e = new Fighter[4];
}
