using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// this is a abstract class that every sequence element (dialogues, choices, item checks/changes...) will inherit from
public abstract class SequenceElement : ScriptableObject {

    public abstract void Trigger();
}
