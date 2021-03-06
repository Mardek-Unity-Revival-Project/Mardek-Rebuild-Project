using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OngoingCommand : CommandBase
{
    public abstract bool IsOngoing();
}
