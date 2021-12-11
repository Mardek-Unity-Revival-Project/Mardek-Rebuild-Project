using UnityEngine;

namespace MURP.StatusSystem
{
    [System.Serializable]
    public class StatusHolder<U, T> where T : StatusOfType<U>
    {
        public StatusHolder(T status)
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