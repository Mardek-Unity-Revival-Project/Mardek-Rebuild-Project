using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MURP
{
    public abstract class MovementController : MonoBehaviour
    {
        [SerializeField] protected Movement movement = null;
        [SerializeField] protected List<MoveDirection> allowedDirections = new List<MoveDirection>();

        public void SendDirection(MoveDirection direction)
        {
            if (movement) movement.MoveInDirectionOnce(direction);
        }

        public MoveDirection ApproximanteDirectionByVector2(Vector2 vector)
        {
            if (vector.sqrMagnitude < Vector2.kEpsilonNormalSqrt)
                return null;
            if (allowedDirections.Count > 0)
            {
                MoveDirection result = allowedDirections[0];
                foreach (MoveDirection dir in allowedDirections)
                {
                    if (Vector2.Distance(result.value, vector) > Vector2.Distance(dir.value, vector))
                    {
                        result = dir;
                    }
                }
                return result;
            }
            return null;
        }
    }
}
