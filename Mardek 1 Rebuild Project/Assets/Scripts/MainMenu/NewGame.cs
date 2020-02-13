using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class NewGame : MonoBehaviour
{
    void OnMouseDown()
    {
        GameProgressData.party[0]="Mardek_Hero";
        GameProgressData.party[1] = "Deugan_Hero";
        GameProgressData.x = 0.08f;
        GameProgressData.y = -0.4f;
        SceneManager.LoadScene("DL_Area1");
    }
}
