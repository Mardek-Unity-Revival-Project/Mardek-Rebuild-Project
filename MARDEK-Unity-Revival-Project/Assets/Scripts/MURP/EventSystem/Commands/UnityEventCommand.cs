using UnityEngine;
using UnityEngine.Events;

namespace MURP.EventSystem
{
    public class UnityEventCommand : CommandBase
    {
        [SerializeField] UnityEvent _event = default;

        public override void Trigger()
        {
            _event.Invoke();
        }
    }
}