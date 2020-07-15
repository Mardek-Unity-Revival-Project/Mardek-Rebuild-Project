using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectThisButton : MonoBehaviour
{
    private GameObject partyButtonGo;
    private Button button;
   
    private void OnEnable()
    {
        partyButtonGo = gameObject;
        button = partyButtonGo.GetComponent<Button>();
        button.Select();
    }
}
