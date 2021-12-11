using UnityEngine;
using MURP.Core;

namespace MURP.StatusSystem
{
    [CreateAssetMenu(menuName = "MURP/StatusSystem/ModifyStatus")]
    public class ModifyStatus : AddressableScriptableObject
    {
        [SerializeField] IntegerStatus targetStatus;
        [SerializeField] int expression;

        public void Apply(IStatus user, IStatus target)
        {
            int value = expression;
            var statusHolder = target.GetStatus(targetStatus);
            statusHolder.Value += value;
        }
    }
}