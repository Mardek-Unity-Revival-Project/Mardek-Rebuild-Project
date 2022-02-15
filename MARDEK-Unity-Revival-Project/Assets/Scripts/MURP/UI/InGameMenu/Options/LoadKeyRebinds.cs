using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LoadKeyRebinds : MonoBehaviour
{
    [SerializeField] InputActionAsset inputActions = null;

    private void Awake()
    {
        foreach (var map in inputActions.actionMaps)
        {
            var bindings = map.bindings;
            for (int i = 0; i < bindings.Count; i++)
            {
                var binding = bindings[i];
                string key = binding.id.ToString();
                string val = PlayerPrefs.GetString(key, null);
                if (string.IsNullOrEmpty(val)) continue;
                map.ApplyBindingOverride(i, new InputBinding { overridePath = val });
            }
        }
    }
}