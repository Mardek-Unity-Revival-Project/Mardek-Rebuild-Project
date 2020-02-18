using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SequenceElement : ScriptableObject
{
    public abstract void Trigger(); /*{
        Debug.Log("Abstract SequenceElement trigger");
    }*/
}
