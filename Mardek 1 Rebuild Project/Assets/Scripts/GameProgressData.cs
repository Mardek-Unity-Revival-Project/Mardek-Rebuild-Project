using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameProgressData
{
    public static Fighter[] p = new Fighter[4];
    public static Fighter[] e = new Fighter[4];
    public static string[] party = { "Deugan_Hero", "Mardek_Hero", "", "" };
    public static float x = 0.08f;
    public static float y = -0.4f;
    public static List<int> Custom_Played = new List<int>();
    public static bool lockdown;
}
