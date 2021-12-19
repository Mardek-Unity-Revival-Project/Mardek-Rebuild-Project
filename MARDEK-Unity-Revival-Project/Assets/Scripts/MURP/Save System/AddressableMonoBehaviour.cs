using UnityEngine;
using System;
using MURP.Core;
using System.Collections.Generic;

namespace MURP.SaveSystem
{
    public class AddressableMonoBehaviour : MonoBehaviour, IAddressableGuid
    {
        static List<AddressableMonoBehaviour> addressablesToSaveOnTransitio = new List<AddressableMonoBehaviour>();

        [SerializeField, FullSerializer.fsIgnore] SaveOptions saveOptions;
        [System.Serializable]
        class SaveOptions {
            public bool loadOnAwake = true;
            public bool autoSave = true;
            public bool saveOnTransition = true;
        }

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
                Debug.Log($"new guid assigned to {this.name}", this);
            }
        }

        private void Awake()
        {
            if (saveOptions.loadOnAwake)
                Load();
        }
        private void OnEnable()
        {
            if(saveOptions.autoSave)
                SaveSystem.OnBeforeSave += Save;
            if (saveOptions.saveOnTransition)
                addressablesToSaveOnTransitio.Add(this);
        }
        private void OnDisable()
        {
            if (saveOptions.autoSave)
                SaveSystem.OnBeforeSave -= Save;
            if (saveOptions.saveOnTransition)
                addressablesToSaveOnTransitio.Remove(this); 
        }
        public static void SaveOnTransition()
        {
            foreach (var o in addressablesToSaveOnTransitio)
                o.Save();
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