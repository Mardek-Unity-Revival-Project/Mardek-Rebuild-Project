using MURP.EventSystem;
using UnityEngine;

namespace MURP.SaveSystem
{
    [System.Serializable]
    public class LocalSwitchBool : AddressableMonoBehaviour, IBoolCheck
    {
        [SerializeField] bool value = false;

        private void Awake()
        {
            Load();
        }

        public bool GetBoolValue()
        {
            return value;
        }

        public void SetBoolValue(bool setValue)
        {
            value = setValue;
            Save();
        }
    }
}