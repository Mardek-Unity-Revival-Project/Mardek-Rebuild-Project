using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct DialogueData
{
    public string text;
    public string header;
    public string element;
    public string font;
    public bool end;
    public DialogueData (string _text, string _header, string _element, string _font, bool _end)
    {
        text = _text;
        header = _header;
        element = _element;
        font = _font;
        end = _end;
    }
}
public class DialogueMaster : MonoBehaviour
{
    DialogueData[] DialogueData;
    int i = -1;
    public Image image;
    public void Update()
    {
        if (GameProgressData.lockdown == false)
        {
            return;
        }
        if (i==-1)
        {
            i++;
            return;
        }
        GameObject go = GameObject.Find("Body"); //finds body in UI
        Text text = go.GetComponent<Text>(); //stores text in variable
        GameObject go2 = GameObject.Find("Header"); //finds header in UI
        Text text2 = go2.GetComponent<Text>(); //stores header text seperately
        GameObject go3 = GameObject.Find("DialogueBox");
        image = go3.GetComponent<Image>();
        image.enabled = true;
        

        text.text = DialogueData[i].text; //writes body text
        text2.text = DialogueData[i].header; //writes header text

        if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.Y) || Input.GetKeyDown(KeyCode.X))
        {
            if (DialogueData[i].end)
            {
                image.enabled = false;
                text.text = ""; //clears body text
                text2.text = ""; //clears header text
                i = -1;
                GameProgressData.lockdown = false;
                return;
            }
            i++;
        }
    }
    public void SendData(DialogueData[] _DialogueData)
    {
        DialogueData = _DialogueData;
        GameProgressData.lockdown = true;
    }
}
