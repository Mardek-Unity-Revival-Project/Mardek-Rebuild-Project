using UnityEngine;
using MURP.Core;

namespace MURP.StatsSystem
{
    [CreateAssetMenu(menuName = "MURP/StatusSystem/ModifyStatus")]
    public class ModifyStat : AddressableScriptableObject
    {
        [SerializeField] IntegerStat targetStatus;

        public void Apply(IStats user, IStats target)
        {
            var statusHolder = target.GetStat(targetStatus);
            Debug.Log(statusHolder.Value);
        }
    }
}