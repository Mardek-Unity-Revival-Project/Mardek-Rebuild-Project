using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCommand : OngoingCommand
{
    [SerializeField] List<MoveDirection> movements;

    public override bool IsOngoing()
    {
        throw new System.NotImplementedException();
    }

    public override void Trigger()
    {
        throw new System.NotImplementedException();
    }
}
