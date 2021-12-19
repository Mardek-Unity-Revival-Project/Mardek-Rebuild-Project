using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MURP.CharacterSystem;

namespace MURP.BattleSystem
{
    public class BattleManager : MonoBehaviour
    {
        public static EncounterSet encounter { private get; set; }
        [SerializeField] Party playerParty;
        List<GameObject> enemies = new List<GameObject>();

        private void Start()
        {
            enemies = encounter.InstantiateEncounter();
            foreach (var character in playerParty.GetCharacters())
                Debug.Log(character.name);
            foreach (var enemy in enemies)
                Debug.Log(enemy.name);
        }
    }
}