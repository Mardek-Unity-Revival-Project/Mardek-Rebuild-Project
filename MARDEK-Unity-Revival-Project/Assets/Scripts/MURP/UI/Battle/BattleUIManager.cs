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

        private void Start()
        {
            foreach(var enemy in battleManager.enemyCharacters)
                CreateCharacterUI(enemyUILayout, enemy);
            foreach(var playerCharacter in battleManager.playerCharacters)
                CreateCharacterUI(partyUILayout, playerCharacter);
        }

        void CreateCharacterUI(RectTransform layout, Character character)
        {
            var ui = Instantiate(characterUIPrefab, layout).GetComponent<CharacterUI>();
            ui.AssignCharacter(character);
        }
    }
}