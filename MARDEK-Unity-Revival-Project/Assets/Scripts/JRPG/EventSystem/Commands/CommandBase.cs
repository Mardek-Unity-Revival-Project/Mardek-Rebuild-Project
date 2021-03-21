using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JRPG
{
    [System.Serializable]
    public abstract class CommandBase
    {
        public abstract void Trigger();
    }
}
