using UnityEngine;

namespace MURP.EventSystem
{
    public class SetBoolCommand : CommandBase
    {
        [SerializeField] Object boolObject;
        [SerializeField] bool setValue;

        public override void Trigger()
        {
            IBoolCheck boolCheck = boolObject as IBoolCheck;
            if (boolCheck != null)
                boolCheck.SetBoolValue(setValue);
            else
                Debug.LogError("boolObject is null or of invalid type");
        }
    }
}