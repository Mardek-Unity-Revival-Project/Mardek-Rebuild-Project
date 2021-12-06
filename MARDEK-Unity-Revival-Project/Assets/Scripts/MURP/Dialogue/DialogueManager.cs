using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MURP.Dialogue
{
    public class DialogueManager : MonoBehaviour
    {
        static DialogueManager instance;
        static bool _isOngoing = false;
        public static bool isOngoing
        {
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
        [SerializeField] TMPro.TMP_Text characterNameText = null;
        [SerializeField] float dialogueSpeed = 5;

        Dialogue dialogue;
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
                if (letterIndex >= 0)
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
            dialogue = null;
        }

        [ContextMenu("OnGoToNextLine")]
        public void OnGoToNextLine()
        {
            if (isOngoing == false)
                return;

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
            if (dialogue == null)
                return string.Empty;

            var characterDialogueLines = dialogue.CharacterLines[dialogueIndex];
            string line = characterDialogueLines.Lines[lineIndex];

            int lengthToShow = line.Length;
            if (letterIndex >= line.Length || letterIndex < 0)
                letterIndex = -1;
            else
                lengthToShow = Mathf.FloorToInt(letterIndex);

            string lineToShow = line.Substring(0, lengthToShow) + "<color=#00000000>";
            lineToShow += line.Substring(lengthToShow, line.Length - lengthToShow) + "</color>";
            return lineToShow;
        }

        bool AdvanceLine()
        {
            lineIndex++;
            var characterDialogueLines = dialogue.CharacterLines[dialogueIndex];
            if (lineIndex >= characterDialogueLines.Lines.Count)
            {
                return AdvanceDialogueList();
            }
            letterIndex = 0;
            return true;
        }

        bool AdvanceDialogueList()
        {
            dialogueIndex++;
            if (dialogueIndex >= dialogue.CharacterLines.Count)
            {
                return false;
            }
            lineIndex = -1;
            UpdateCharacterName();
            return AdvanceLine();
        }

        private void UpdateCharacterName()
        {
            if (dialogue != null)
            {
                CharacterBio characterBio = dialogue.CharacterLines[dialogueIndex].Character;
                if (characterBio != null)
                {
                    characterNameText.text = characterBio.displayName;
                    return;
                }
            }
            characterNameText.text = "";
        }

        void EndDialogue()
        {
            ResetManager();
            UpdateCharacterName();
            UpdateUI();
            canvas.SetActive(false);
        }

        void UpdateUI()
        {
            dialogueText.text = CurrentLine();
        }

        public static void EnqueueDialogue(Dialogue _dialogue)
        {
            if (instance == null)
            {
                Debug.LogWarning("No DialogueManager instance found");
            }
            else
            {
                instance.dialogue = _dialogue;
                if (isOngoing == false)
                    instance.TryStartDialogues();
            }
        }
    }
}