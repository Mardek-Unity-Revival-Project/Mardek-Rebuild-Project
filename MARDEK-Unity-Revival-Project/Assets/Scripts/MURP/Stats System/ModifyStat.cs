using UnityEngine;
using MURP.Core;

namespace MURP.StatsSystem
{
    [CreateAssetMenu(menuName = "MURP/StatusSystem/ModifyStatus")]
    public class ModifyStat : AddressableScriptableObject
    {
        [SerializeField] IntegerStat targetStatus;
        [SerializeField] int expression;

        public void Apply(IStats user, IStats target)
        {
            int value = expression;
            var statusHolder = target.GetStatus(targetStatus);
            statusHolder.Value += value;
        }
    }
}