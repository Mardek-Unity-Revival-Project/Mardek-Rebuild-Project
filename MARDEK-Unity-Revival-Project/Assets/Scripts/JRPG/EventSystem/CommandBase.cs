using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CommandBase : ScriptableObject
{
    public abstract void Trigger();
}
