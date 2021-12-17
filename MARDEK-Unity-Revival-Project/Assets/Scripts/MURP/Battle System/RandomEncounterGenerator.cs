using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MURP.MovementSystem;
using UnityEngine.Events;
//using MURP.EventSystem;

namespace MURP.BattleSystem
{
    public class RandomEncounterGenerator : MonoBehaviour
    {
        [SerializeField] Movable movable;
        [SerializeField] int minSteps = 20;
        [SerializeField] int maxSteps = 30;
        [SerializeField] UnityEvent onTriggerBattle;
        int steps = 0;
        int requiredSteps;

        private void Start()
        {
            GenerateRequiredSteps();
            movable.OnEndMove += Step;
        }
        void GenerateRequiredSteps()
        {
            requiredSteps = Random.Range(minSteps, maxSteps + 1);
        }
        void Step()
        {
            if (PlayerController.isPlayerLocked)
                return;
            steps++;
            if (steps < requiredSteps)
                return;
            steps = 0;
            GenerateRequiredSteps();
            TriggerBattle();
        }
        void TriggerBattle()
        {
            Debug.Log("TRIGGER BATTLE");
            onTriggerBattle.Invoke();
        }
    }
}