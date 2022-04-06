using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MURP.SkillSystem;

namespace MURP.CharacterSystem
{
    [System.Serializable]
    public class SkillSlot
    {
        [field: SerializeField] public Skill Skill {get;set;}
        [field: SerializeField] public int MasteryPoints { get; set; }
        public bool IsMastered
        {
            get
            {
                var points = MasteryPoints;
                var requiredPoints = Skill.PointsRequiredToMaster;
                return ((points >= requiredPoints) || (points == -1));
            }
        }
    }
}