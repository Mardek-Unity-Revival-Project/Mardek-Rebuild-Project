using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MURP.CharacterSystem;
using MURP.StatsSystem;
using MURP.SkillSystem;

namespace MURP.BattleSystem
{
    public class BattleManager : MonoBehaviour
    {
        public static EncounterSet encounter { private get; set; }
        [SerializeField, HideInInspector] FloatStat ACTStat = null;
        [SerializeField, HideInInspector] IntegerStat AGLStat = null;
        const float actResolution = 10;
        [SerializeField] Party playerParty;
        List<GameObject> enemies = new List<GameObject>();
        [SerializeField] GameObject actionPickerUI = null;

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

        public static Character characterActing { get; private set; }
        public static SkillSlot selectedSkill { get; set; }
        public static List<Character> targets { get; private set; }

        private void Awake()
        {
            if(encounter) enemies = encounter.InstantiateEncounter();
        }
        private void Update()
        {
            if (characterActing == null)
            {
                characterActing = StepActCycleTryGetNextCharacter();
                if (characterActing)
                {
                    actionPickerUI.SetActive(true);
                }
            }
            else
            {
                // resolve action
                if (selectedSkill != null)
                {
                    if(targets != null)
                    {
                        foreach (var t in targets)
                            Debug.Log($"{characterActing.name} uses {selectedSkill.Skill.DisplayName} on {t.name}");
                        targets = null;
                        selectedSkill = null;
                        characterActing = null;
                    }
                }
            }
        }
        Character StepActCycleTryGetNextCharacter()
        {
            var charactersInBattle = GetCharactersInOrder();
            AddTickRateToACT(ref charactersInBattle, Time.deltaTime);
            var readyToAct = GetNextCharacterReadyToAct(charactersInBattle);
            if (readyToAct != null)
                readyToAct.ModifyStat(ACTStat, -actResolution); // "reset" charact ACT
            return readyToAct;
        }
        List<Character> GetCharactersInOrder()
        {
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
                var tickRate = 1 + 0.05f * c.GetStat(AGLStat).Value;
                tickRate *= deltatime;
                c.ModifyStat(ACTStat, tickRate);
            }
        }
        Character GetNextCharacterReadyToAct(List<Character> characters)
        {
            float maxAct = 0;
            foreach(var c in characters)
            {
                var act = c.GetStat(ACTStat).Value;
                if (act > maxAct)
                    maxAct = act;
            }
            if (maxAct < actResolution)
                return null;
            foreach (var c in characters)
                if (c.GetStat(ACTStat).Value == maxAct)
                    return c;
            throw new System.Exception("A character had enough ACT to take a turn but wasn't returned by this method");
        }
    }
}