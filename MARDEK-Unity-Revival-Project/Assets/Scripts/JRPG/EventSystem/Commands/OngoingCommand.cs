using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JRPG
{
    public abstract class OngoingCommand : CommandBase
    {
        [SerializeField] bool _waitExcecutionEnd = true;
        public bool waitForExecutionEnd { get { return _waitExcecutionEnd; } }
        public abstract bool IsOngoing();
        public virtual void UpdateCommand()
        {

        }
    }
}
