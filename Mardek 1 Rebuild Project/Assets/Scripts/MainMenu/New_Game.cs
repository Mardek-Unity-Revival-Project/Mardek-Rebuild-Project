using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class New_Game : MonoBehaviour
{
    void OnMouseDown()
    {
        GameFile.x = 0.08f;
        GameFile.y = -0.4f;
        GameFile.party_members = new string[] { "Mardek_Hero", "Deugan_Hero", "", ""};
        SceneManager.LoadScene("DL_Area1");
    }
}
