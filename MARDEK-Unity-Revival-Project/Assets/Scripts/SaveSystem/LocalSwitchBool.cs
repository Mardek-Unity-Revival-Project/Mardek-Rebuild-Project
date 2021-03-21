using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LocalSwitchBool : AddressableMB, IBoolCheck
{
    [SerializeField] bool value = false;

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
