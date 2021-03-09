﻿using UnityEngine;
using System.Collections.Generic;

[SelectionBase]
public class InMapParty : MonoBehaviour
{
    static InMapParty instance;

    [SerializeField] List<GameObject> inMapCharacters = new List<GameObject>();

    private void Awake()
    {
        if (instance)
            Destroy(instance);
        instance = this;
    }

    public static void PositionPartyAt(Vector2 position)
    {
        if (instance)
        {
            for(int i = 0; i < instance.inMapCharacters.Count; i++)
            {
                instance.inMapCharacters[i].transform.position = position;
                GameFile.AddProgress(1);
            }
        }
        else
        {
            Debug.LogError("No InMapParty found");
        }
    }
}
