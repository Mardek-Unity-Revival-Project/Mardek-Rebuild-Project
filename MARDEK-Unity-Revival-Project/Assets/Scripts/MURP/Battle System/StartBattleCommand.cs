using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MURP.EventSystem;
using UnityEngine.SceneManagement;
using MURP.SaveSystem;

namespace MURP.BattleSystem
{
    public class StartBattleCommand : CommandBase
    {
        [SerializeField] EncounterSet encounter = null;
        [SerializeField] SceneReference battleScene = default;

        public override void Trigger()
        {
            BattleManager.encounter = encounter;
            SceneManager.LoadScene(battleScene);
        }
    }
}