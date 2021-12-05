using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

namespace JRPG
{
    public class ChoicesManager : MonoBehaviour
    {
        static ChoicesManager instance;
        int chosenIndex = -1;

        [SerializeField] GameObject canvas = null;
        [SerializeField] RectTransform layoutGroup = null;
        [SerializeField] List<GameObject> choicesUIObjects = new List<GameObject>();

        private void Awake()
        {
            instance = this;
        }

        public static void TriggerChoices(Dialogue dialogue)
        {
            instance.canvas.SetActive(true);
            instance.StartCoroutine(instance.SetupChoices(dialogue));
        }

        IEnumerator SetupChoices(Dialogue dialogue)
        {
            for(int i = 0; i < dialogue.CharacterLines[0].Lines.Count; i++)
            {
                var text = dialogue.CharacterLines[0].Lines[i];
                choicesUIObjects[i].GetComponent<Text>().text = text;
                choicesUIObjects[i].SetActive(true);
            }
            LayoutRebuilder.ForceRebuildLayoutImmediate(layoutGroup);
            yield return new WaitForEndOfFrame();
            choicesUIObjects[0].GetComponent<Button>().Select();
        }
        
        public void Choose()
        {
            for(int i = 0; i < choicesUIObjects.Count; i++)
            {                
                if(EventSystem.current.currentSelectedGameObject == choicesUIObjects[i])
                {
                    chosenIndex = i;
                    break;
                }
            }
        }

        public static int GetChosenIndex()
        {
            int value = instance.chosenIndex;
            if(value > -1)
            {
                ResetChoiceManager();
            }
            return value;
        }

        public static void ResetChoiceManager()
        {
            instance.chosenIndex = -1;
            foreach (var choice in instance.choicesUIObjects)
                choice.SetActive(false);
            instance.canvas.SetActive(false);
            EventSystem.current.SetSelectedGameObject(null);
        }
    }
}
