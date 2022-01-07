using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MURP.BattleSystem;
using MURP.CharacterSystem;

namespace MURP.UI
{
    public class BattleUIManager : MonoBehaviour
    {
        [SerializeField] BattleManager battleManager;
        [SerializeField] GameObject characterUIPrefab;
        [SerializeField] RectTransform enemyUILayout;
        [SerializeField] RectTransform partyUILayout;

        [SerializeField] RectTransform turnUILayout;
        [SerializeField] RectTransform turnUIListParent;

        [SerializeField] RectTransform actionsUILayout;
        [SerializeField] RectTransform powersUILayout;
        [SerializeField] RectTransform reactUILayout;
        [SerializeField] RectTransform victoryUILayout;
        [SerializeField] RectTransform defeatUILayout;

        enum UIStates {
            Idle,
            Actions,
            Powers,
            Targeting,
            Animation,
            Victory,
            Defeat
        };
        UIStates currentUIState = UIStates.Idle;

        private void Start()
        {
            foreach(var enemy in battleManager.enemyCharacters)
                CreateCharacterUI(enemyUILayout, enemy);
            foreach(var playerCharacter in battleManager.playableCharacters)
                CreateCharacterUI(partyUILayout, playerCharacter);
            UpdateTurnOrder(turnUIListParent);
        }

        void Update() {
            if (battleManager == null) {
                return;
            }

            if (currentUIState == UIStates.Idle) {
                if (battleManager.IsPlayerTurn()) {
                    SetUIState(UIStates.Actions);
                }
            }
        }

        void SetUIState(UIStates newState) {
            // remove the old UI elements
            switch (currentUIState) {
                case UIStates.Actions:
                    // TODO hide the turnUI
                    break;
            }

            currentUIState = newState;

            // show the new UI elements
            switch (currentUIState) {
                case UIStates.Actions:
                    // TODO play animation to show the actionsUI and turnUI
                    UpdateTurnOrder(turnUIListParent);
                    break;

                case UIStates.Powers:
                    break;
            }
        }

        void CreateCharacterUI(RectTransform layout, Character character)
        {
            var ui = Instantiate(characterUIPrefab, layout).GetComponent<CharacterUI>();
            ui.AssignCharacter(character);
        }

        void UpdateTurnOrder(RectTransform parent)
        {
            var characterTurnsList = battleManager.GetCharactersInTurnOrder(parent.childCount);
            Debug.Log(characterTurnsList.Count);
            for(int i=0; i < parent.childCount; i++) {
                parent.GetChild(i).GetComponent<TurnIconUI>().AssignCharacter(characterTurnsList[i]);
            }
        }
    }
}