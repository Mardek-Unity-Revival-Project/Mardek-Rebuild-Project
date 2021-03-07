using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JRPG
{
    public abstract class MovementController : MonoBehaviour
    {
        [SerializeField] protected Movement controlledMovement = null;
        [SerializeField] protected List<MoveDirection> allowedDirections = new List<MoveDirection>();
    }
}
