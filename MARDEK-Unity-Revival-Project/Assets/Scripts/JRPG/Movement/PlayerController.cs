using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace JRPG
{
    public class PlayerController : MovementController
    {
        public static int playerControllerLockValue;

        MoveDirection desiredDirection = null;

        private void Awake()
        {
            playerControllerLockValue = 0;
        }

        public void OnInteraction(InputAction.CallbackContext ctx)
        {
            if (playerControllerLockValue > 0)
                return;

            if (ctx.performed == false)
                return;
            if(movement == null || movement.isMoving || movement.currentDirection == null)
                return;

            movement.colliderHelper.OffsetCollider(movement.currentDirection.value);

            List<Collider2D> collidersHit = movement.colliderHelper.Overlaping();
            foreach(Collider2D c in collidersHit)
            {
                JRPGEventTrigger trigger = c.GetComponent<JRPGEventTrigger>();
                if (trigger) trigger.Interact();
            }
            //return collider to place
            movement.colliderHelper.OffsetCollider(Vector2.zero);
        }
        
        public void OnMovementInput(InputAction.CallbackContext ctx)
        {
            Vector2 direction = ctx.ReadValue<Vector2>();

            if (direction.x == 0 || direction.y == 0)
                desiredDirection = ApproximanteDirectionByVector2(direction);
            else
                desiredDirection = null;
        }

        //late update to avoid race conditions with the input system calls
        private void Update()
        {
            if (playerControllerLockValue > 0)
                return;
            SendDirection(desiredDirection);
        }
    }
}
