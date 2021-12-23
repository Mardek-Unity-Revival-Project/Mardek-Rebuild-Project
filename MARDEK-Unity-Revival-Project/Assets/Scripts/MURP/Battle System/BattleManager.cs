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

        public List<Character> playerCharacters
        {
            get
            {
                return playerParty.Characters;
            }
        }
        public List<Character> enemyCharacters
        {
            get
            {
                var chars = new List<Character>();
                foreach (var enemy in enemies)
                    chars.Add(enemy.GetComponent<Character>());
                return chars;
            }
        }

        private void Awake()
        {
            if(encounter)
                enemies = encounter.InstantiateEncounter();
        }
    }
}