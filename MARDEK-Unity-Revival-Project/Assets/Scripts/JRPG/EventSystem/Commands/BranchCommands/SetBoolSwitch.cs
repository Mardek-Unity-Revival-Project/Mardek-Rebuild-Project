using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JRPG
{
    public class SetBoolSwitch : CommandBase
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
