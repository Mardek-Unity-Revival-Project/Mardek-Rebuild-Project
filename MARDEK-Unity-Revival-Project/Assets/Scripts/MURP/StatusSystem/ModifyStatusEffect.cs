using UnityEngine;
using MURP.Core;

namespace MURP.StatusSystem
{
    [CreateAssetMenu(menuName = "MURP/ModifyStatusEffect")]
    public class ModifyStatusEffect : AddressableScriptableObject
    {
        [SerializeField] IntegerStatus targetStatus;
        [SerializeField] int expression;

        public void Apply(IAffectable user, IAffectable target)
        {
            int value = expression;
            var statusHolder = target.GetStatus(targetStatus);
            statusHolder.Value += value;
        }
    }
}