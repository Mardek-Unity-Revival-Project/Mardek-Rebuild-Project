using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class selectPartyButton : MonoBehaviour
{
    private GameObject partyButtonGo;
    private Button button;
    // Start is called before the first frame update
    /*void Start()
    {
        partyButtonGo = GameObject.Find("PartyButton");
        button = partyButtonGo.GetComponent<Button>();
        button.Select();
    }*/

    /*private void Awake()
    {
        partyButtonGo = GameObject.Find("PartyButton");
        button = partyButtonGo.GetComponent<Button>();
        button.Select();
    }*/

    private void OnEnable()
    {
        partyButtonGo = GameObject.Find("PartyButton");
        button = partyButtonGo.GetComponent<Button>();
        button.Select();
    }
}
