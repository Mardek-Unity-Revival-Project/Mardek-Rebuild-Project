using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDialogue : X_Interaction
{

    private const int SIZE = 0;
    public int size;
    public string[] text = new string[SIZE];
    public string[] header = new string[SIZE];
    public string[] element = new string[SIZE];
    public string[] font = new string[SIZE];
    private DialogueData[] DialogueData;

    public override void DoAction()
    {
        DialogueData[] DialogueData = new DialogueData[size];
        for (int i = 0; i < size - 1; i++)
        {
            DialogueData[i] = new DialogueData(text[i], header[i], element[i], font[i], false);
        }
        DialogueData[size - 1] = new DialogueData(text[size - 1], header[size - 1], element[size - 1], font[size - 1], true);
        GameObject go = GameObject.Find("UI");
        DialogueMaster DialogueMaster = go.GetComponent<DialogueMaster>();
        DialogueMaster.SendData(DialogueData);
    }
}
