using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace JRPG
{
    public class PlayerMovementController : MovementController
    {
        MoveDirection desiredDirection = null;

        public void OnMovementInput(InputAction.CallbackContext ctx)
        {
            Vector2 direction = ctx.ReadValue<Vector2>();
            if (direction.x == 0 || direction.y == 0)
                desiredDirection = AproximanteDirectionByVector2(direction);
                
        }

        MoveDirection AproximanteDirectionByVector2(Vector2 vector)
        {
            if (vector == Vector2.zero)
                return null;
            if(allowedDirections.Count > 0)
            {
                MoveDirection result = allowedDirections[0];
                foreach(MoveDirection dir in allowedDirections)
                {
                    if(Vector2.Distance(result.value, vector) > Vector2.Distance(dir.value, vector))
                    {
                        result = dir;
                    }
                }
                return result;
            }
            return null;
        }

        private void Update()
        {
            if(desiredDirection != null)
            {
                controlledMovement.Move(desiredDirection);
                //desiredDirection = null;
            }
        }
    }
}
