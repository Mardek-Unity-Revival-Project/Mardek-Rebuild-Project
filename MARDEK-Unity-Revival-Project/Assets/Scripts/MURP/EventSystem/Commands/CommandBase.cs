using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MURP
{
    //[System.Serializable]
    public abstract class CommandBase : MonoBehaviour
    {
        public abstract void Trigger();
    }
}