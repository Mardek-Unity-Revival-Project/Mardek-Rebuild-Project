using MURP.EventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
