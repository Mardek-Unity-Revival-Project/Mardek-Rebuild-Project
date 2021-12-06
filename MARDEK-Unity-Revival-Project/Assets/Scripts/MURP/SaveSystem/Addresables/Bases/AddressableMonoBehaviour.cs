using UnityEngine;
using System;

namespace MURP.SaveSystem
{
    public class AddressableMonoBehaviour : MonoBehaviour, IAddressableGuid
    {
        [SerializeField, HideInInspector, FullSerializer.fsIgnore]
        private byte[] serializedGuid;
        Guid guid
        {
            get
            {
                if (serializedGuid == null || serializedGuid.Length != 16)
                    return Guid.Empty;
                return new Guid(serializedGuid);
            }
            set { serializedGuid = value.ToByteArray(); }
        }

        public Guid GetGuid() { return guid; }

        private void OnValidate()
        {
            if (guid == Guid.Empty)
            {
                guid = Guid.NewGuid();
                Debug.Log("new guid assigned");
            }
        }

        public virtual void Save()
        {
            SaveSystem.SaveObject(this);
        }
        public virtual void Load()
        {
            SaveSystem.LoadObject(this);
        }

        [ContextMenu("Save")]
        void SaveWrapper()
        {
            Save();
        }
        [ContextMenu("Load")]
        void LoadWrapper()
        {
            Load();
        }
    }
}