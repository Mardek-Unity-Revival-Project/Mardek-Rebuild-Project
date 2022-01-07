using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MURP.CharacterSystem;
using MURP.StatsSystem;

namespace MURP.BattleSystem
{
    public class BattleManager : MonoBehaviour
    {
        public static EncounterSet encounter { private get; set; }
        [SerializeField, HideInInspector] FloatStat ACTStat = null;
        [SerializeField, HideInInspector] IntegerStat AGLStat = null;
        const float actRequired = 10; // Is this required or can we could down to zero?
        [SerializeField] Party playerParty;
        List<GameObject> enemies = new List<GameObject>();

        public List<Character> playableCharacters
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
            if(encounter) enemies = encounter.InstantiateEncounter();
        }

        private void Update()
        {
            var charactersInBattle = GetCharactersInOrder();
            AddTickRateToACT(ref charactersInBattle, Time.deltaTime);
            var readyToAct = GetNextCharacterReadyToAct(charactersInBattle);
            if (readyToAct != null)
            {
                Debug.Log($"{readyToAct.name} should act");
                // TODO: pause this update if character is playable
                if (playableCharacters.Contains(readyToAct))
                    readyToAct.BattleAct(allies: playableCharacters, enemies: enemyCharacters);
                else
                    readyToAct.BattleAct(allies: enemyCharacters, enemies: playableCharacters);
                // "reset" characters' ACT
                readyToAct.ModifyStat(ACTStat, -actRequired); // Is this required or can we could down to zero?
            }
        }

        public bool IsPlayerTurn() {
            return IsPlayerTurn(GetCharactersInOrder());
        }

        public bool IsPlayerTurn(List<Character> characters) {
            if (playableCharacters.Contains(GetNextCharacterReadyToAct(characters)))
                return true;
            else
                return false;
        }

        List<Character> GetCharactersInOrder() {
            // order by position (p1 e1 p2 e2 p3 e3 p4 e4)
            List<Character> returnList = new List<Character>();
            for (int i = 0; i < 4; i++)
            {
                if (playableCharacters.Count > i)
                    returnList.Add(playableCharacters[i]);
                if (enemyCharacters.Count > i)
                    returnList.Add(enemyCharacters[i]);
            }
            return returnList;
        }

        void AddTickRateToACT(ref List<Character> characters, float deltatime)
        {
            foreach(var c in characters)
            {
                var tickRate = 1f + 0.05f * c.GetStat(AGLStat).Value;
                tickRate *= deltatime;
                c.ModifyStat(ACTStat, tickRate);
            }
        }

        Character GetNextCharacterReadyToAct(List<Character> characters)
        {
            return GetCharactersInTurnOrder(characters, 1)[0];
        }

        public List<Character> GetCharactersInTurnOrder(int turns) {
            return GetCharactersInTurnOrder(GetCharactersInOrder(), turns);
        }

        public List<Character> GetCharactersInTurnOrder(List<Character> characters, int turns) {
            List<Character> returnList = new List<Character>();

            float[] tempAct = new float[characters.Count];
            for (int i=0; i < characters.Count; i++) {
                tempAct[i] = characters[i].GetStat(ACTStat).Value;
            }

            for (int i = 0; i < turns; i++) {

                // determine next character, with the minimum time left to reach required act
                int nextCharacterIndex = 0;
                float minTimeLeft = 1e5f;
                for (int j = 0; j < tempAct.Length; j++) {
                    float timeLeft = (actRequired - tempAct[j]) / (1f + 0.05f * characters[j].GetStat(AGLStat).Value);
                    if (timeLeft < minTimeLeft) {
                        minTimeLeft = timeLeft;
                        nextCharacterIndex = j;
                    }
                }

                returnList.Add(characters[nextCharacterIndex]);
                tempAct[nextCharacterIndex] -= actRequired;

                // simulate the minimum time passing for all characters
                for (int j = 0; j < tempAct.Length; j++) {
                    tempAct[j] += minTimeLeft / (1f + 0.05f * characters[j].GetStat(AGLStat).Value);
                }
            }
            return returnList;
        }
    }
}