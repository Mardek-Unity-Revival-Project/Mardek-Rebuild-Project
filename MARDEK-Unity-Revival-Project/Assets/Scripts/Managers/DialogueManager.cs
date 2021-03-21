using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JRPG;

public class DialogueManager : MonoBehaviour
{
    static DialogueManager instance;
    static bool _isOngoing = false;
    public static bool isOngoing { 
        get 
        {
            if (instance == null)
                return false;
            else
                return _isOngoing;
        } 
        private set 
        {
            _isOngoing = value;
        } 
    }
    
    [SerializeField] GameObject canvas = null;
    [SerializeField] TMPro.TMP_Text dialogueText = null;
    [SerializeField] float dialogueSpeed = 5;

    List<Dialogue> dialogues;
    int dialogueIndex = 0;
    int lineIndex = 0;
    float letterIndex = 0;
    bool isSkipping = false;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            ResetManager();
        }
    }

    private void Update()
    {
        if (isOngoing)
        {
            if(letterIndex >= 0)
                letterIndex += Time.deltaTime * dialogueSpeed;
            UpdateUI();
            if (isSkipping)
                OnGoToNextLine();
        }
    }

    private void ResetManager()
    {
        isOngoing = false; 
        isSkipping = false;
        dialogueIndex = 0;
        lineIndex = -1;
        dialogues = new List<Dialogue>();
    }

    [ContextMenu("OnGoToNextLine")]
    public void OnGoToNextLine()
    {
        if (letterIndex < 0)
        {
            if (AdvanceLine() == false)
                EndDialogue();
        }
        else
            letterIndex = -1;
    }

    public void SetSkipping(bool value)
    {
        isSkipping = value;
    }

    void TryStartDialogues()
    {
        dialogueIndex = -1;
        if (AdvanceDialogueList())
        {
            isOngoing = true;
            canvas.SetActive(true);
            UpdateUI();
        }
    }

    string CurrentLine()
    {
        var linesInDialogue = dialogues[dialogueIndex].GetLines();
        string FullLine = linesInDialogue[lineIndex];

        int lengthToShow = FullLine.Length;
        if (letterIndex >= FullLine.Length || letterIndex < 0)
            letterIndex = -1;
        else
            lengthToShow = Mathf.FloorToInt(letterIndex);

        string lineToShow = FullLine.Substring(0, lengthToShow) + "<color=#00000000>";
        lineToShow += FullLine.Substring(lengthToShow, FullLine.Length - lengthToShow) + "</color>";
        return lineToShow;
    }

    bool AdvanceLine()
    {
        lineIndex++;
        var lines = dialogues[dialogueIndex].GetLines();
        if (lineIndex >= lines.Count)
        {
            return AdvanceDialogueList();
        }
        letterIndex = 0;
        return true;
    }

    bool AdvanceDialogueList()
    {
        dialogueIndex++;
        if(dialogueIndex >= dialogues.Count)
        {
            return false;
        }            
        lineIndex = -1;
        return AdvanceLine();
    }

    void EndDialogue()
    {
        ResetManager();
        canvas.SetActive(false);
    }

    void UpdateUI()
    {
        dialogueText.text = CurrentLine();
    }
    
    public static void EnqueueDialogue(List<Dialogue> dialogues)
    {
        if (instance == null)
        {
            Debug.LogWarning("No DialogueManager instance found");
            foreach (Dialogue d in dialogues)
                foreach(string s in d.GetLines())
                Debug.Log(s);
        }
        else
        {
            instance.dialogues = dialogues;
            if (isOngoing == false)
                instance.TryStartDialogues();
        }
    }
}