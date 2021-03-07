using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    List<Dialogue> dialogues;
    int dialogueIndex = 0;
    int lineIndex = 0;
    
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    [ContextMenu("OnGoToNextLine")]
    public void OnGoToNextLine()
    {
        if(AdvanceLine())
            UpdateUI();
        else
            EndDialogue();
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
        return linesInDialogue[lineIndex];
    }

    bool AdvanceLine()
    {
        lineIndex++;
        var lines = dialogues[dialogueIndex].GetLines();
        if (lineIndex >= lines.Count)
        {
            return AdvanceDialogueList();
        }
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
        isOngoing = false;
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

    //void GetNextLine()
    //{
    //    while(dialogues.Count > 0)
    //    {
    //        var lines = dialogues[0].GetLines();
    //        while(lines.Count > 0)
    //        {                
    //            var line = lines[0];
    //            if (!string.IsNullOrEmpty(line))
    //            {
    //                if(currentLine == null)
    //                {
    //                    currentLine = line;
    //                    return;
    //                }
    //                else
    //                {
    //                    currentLine = null;
    //                }
    //            }
    //            lines.RemoveAt(0);
    //        }
    //        dialogues.RemoveAt(0);
    //    }
    //    currentLine = null;
    //    return;
    //}