using UnityEngine;

namespace MURP.EventSystem
{
    public abstract class CommandBase : MonoBehaviour
    {
        public abstract void Trigger();
    }
}