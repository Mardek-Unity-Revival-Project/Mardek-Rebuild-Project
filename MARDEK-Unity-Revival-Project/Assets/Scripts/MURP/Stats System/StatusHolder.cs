using UnityEngine;

namespace MURP.StatsSystem
{
    [System.Serializable]
    public class StatHolder<U, T> where T : StatOfType<U>
    {
        public StatHolder(T status)
        {
            statusEnum = status;
        }

        [field: SerializeField] public T statusEnum { get; private set; }
        [SerializeField] U _value = default;
        public U Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }
    }
}