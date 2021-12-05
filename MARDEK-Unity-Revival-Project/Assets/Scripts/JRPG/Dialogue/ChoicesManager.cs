using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JRPG
{
    public class ChoicesManager : MonoBehaviour
    {
        static ChoicesManager instance;

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
            instance.ShowChoices(dialogue);
        }

        void ShowChoices(Dialogue dialogue)
        {
            for(int i = dialogue.CharacterLines[0].Lines.Count-1; i >= 0 ; i--)
            {
                var text = dialogue.CharacterLines[0].Lines[i];
                choicesUIObjects[i].GetComponent<Text>().text = text;
                choicesUIObjects[i].SetActive(true);
            }
            LayoutRebuilder.ForceRebuildLayoutImmediate(layoutGroup);
        }

        void HideChoices()
        {
            foreach(var choiceUI in choicesUIObjects)
            {
                choiceUI.SetActive(false);
            }
        }
        
        public void Choose(int i)
        {

        }
    }
}
