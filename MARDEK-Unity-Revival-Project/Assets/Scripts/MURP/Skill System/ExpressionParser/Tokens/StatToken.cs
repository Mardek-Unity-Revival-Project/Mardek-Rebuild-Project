using MURP.StatsSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MURP.SkillSystem.ExpressionParser
{
    public class StatToken : ValueToken
    {
        StatBase _targetStat;
        bool _getFromTarget;

        public StatToken(StatBase targetStat, bool getFromTarget)
        {
            _targetStat = targetStat;
            _getFromTarget = getFromTarget;
        }

        public override float Evaluate(IStats user, IStats target)
        {
            IStats statHolderToGetFrom = _getFromTarget ? target : user;
            if (_targetStat is StatOfType<int>)
                return statHolderToGetFrom.GetStat(_targetStat as StatOfType<int>).Value;
            if (_targetStat is StatOfType<float>)
                return statHolderToGetFrom.GetStat(_targetStat as StatOfType<float>).Value;
            Debug.LogError("Tried to get unsuported type of stat");
            return 0;
        }
    }
}