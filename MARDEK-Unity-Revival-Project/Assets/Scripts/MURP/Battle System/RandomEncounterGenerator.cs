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
        int stepsTaken = 0;
        int requiredSteps;

        private void Start()
        {
            GenerateRequiredSteps();
            movable.OnEndMove += Step;
        }
        private void LateUpdate()
        {
            if (stepsTaken < requiredSteps)
                return;
            stepsTaken = 0;
            GenerateRequiredSteps();
            TriggerBattle();
        }
        void TriggerBattle()
        {
            Debug.Log("TRIGGER BATTLE");
            onTriggerBattle.Invoke();
        }
        void Step()
        {
            if (PlayerController.isPlayerLocked)
                return;
            stepsTaken++;
        }
        void GenerateRequiredSteps()
        {
            requiredSteps = Random.Range(minSteps, maxSteps + 1);
        }
    }
}