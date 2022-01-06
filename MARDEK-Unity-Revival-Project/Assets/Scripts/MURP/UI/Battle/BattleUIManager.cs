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

        private void Start()
        {
            foreach(var enemy in battleManager.enemyCharacters)
                CreateCharacterUI(enemyUILayout, enemy);
            foreach(var playerCharacter in battleManager.playableCharacters)
                CreateCharacterUI(partyUILayout, playerCharacter);
            UpdateTurnUI(turnUILayout);
        }

        void CreateCharacterUI(RectTransform layout, Character character)
        {
            var ui = Instantiate(characterUIPrefab, layout).GetComponent<CharacterUI>();
            ui.AssignCharacter(character);
        }

        void UpdateTurnUI(RectTransform layout)
        {
            var characterTurnsList = battleManager.GetCharactersInTurnOrder(layout.childCount);
            Debug.Log(characterTurnsList.Count);
            for(int i=0; i < layout.childCount; i++) {
                layout.GetChild(i).GetComponent<TurnIconUI>().AssignCharacter(characterTurnsList[i]);
            }
        }
    }
}