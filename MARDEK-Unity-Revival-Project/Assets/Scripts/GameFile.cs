using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameFile : ScriptableObject
{
    static GameFile instance;

    [SerializeField] int progress = 0;

    private void OnEnable()
    {
        if (instance == null)
        {
            //Debug.Log("Assigning GameFile static instance");
            instance = this;
        }
        else
        {
            DestroyImmediate(this);
        }
    }

    public static void AddProgress(int i)
    {
        instance.progress += i;
    }

    [ContextMenu("LogInstance")]
    void DebugLogInstance()
    {
        Debug.Log(instance);
    }
}
