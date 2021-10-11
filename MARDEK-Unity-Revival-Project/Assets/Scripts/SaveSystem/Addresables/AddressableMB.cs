using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.Experimental.SceneManagement;
#endif

public class AddressableMB : MonoBehaviour, IAddressableGuid
{
    [SerializeField,HideInInspector,FullSerializer.fsIgnore]
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
        if(guid == Guid.Empty)
        {
            guid = Guid.NewGuid();
            Debug.Log("new guid assigned");
        }
    }

    private void Awake()
    {
        Load();
    }

    [ContextMenu("Save")]
    void SaveWrapper()
    {
        Save();
    }

    public virtual void Save()
    {
        SaveSystem.SaveObject(this);
    }

    [ContextMenu("Load")]
    void LoadWrapper()
    {
        Load();
    }

    public virtual void Load()
    {
        SaveSystem.LoadObject(this);
    }
}
