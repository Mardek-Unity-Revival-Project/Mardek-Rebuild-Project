using MURP.EventSystem;
using UnityEngine;

namespace MURP.SaveSystem
{
    [System.Serializable]
    public class LocalSwitchBool : AddressableMonoBehaviour, IBoolCheck
    {
        [SerializeField] bool value = false;

        public bool GetBoolValue()
        {
            return value;
        }

        public void SetBoolValue(bool setValue)
        {
            value = setValue;
        }
    }
}