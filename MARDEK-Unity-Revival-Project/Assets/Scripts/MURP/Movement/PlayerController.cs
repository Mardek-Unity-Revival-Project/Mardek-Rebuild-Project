using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using MURP.Core;
using MURP.EventSystem;
using MURP.UI;
using Event = MURP.EventSystem.Event;

namespace MURP.Movement
{
    public class PlayerController : MovementController
    {
        static PlayerController instance;
        MoveDirection desiredDirection = null;

        bool isPlayerLocked
        {
            get
            {                
                return CommandQueue.lockValue > 0 || InGameMenu.lockValue;
            }
        }

        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Debug.LogError("there was already a PlayerController intance when a new PlayerController awoke");
        }

        public static Movable GetPlayerMovement()
        {
            if (instance)
                return instance.movement;
            else
                return null;
        }

        public void OnInteraction(InputAction.CallbackContext ctx)
        {
            if (isPlayerLocked)
                return;

            if (ctx.performed == false)
                return;
            if(movement == null || movement.isMoving || movement.currentDirection == null)
                return;

            movement.colliderHelper.OffsetCollider(movement.currentDirection.value);

            List<Collider2D> collidersHit = movement.colliderHelper.Overlaping();
            foreach(Collider2D c in collidersHit)
            {
                Event ev = c.GetComponent<Event>();
                if (ev) ev.Interact();
            }
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

        private void Update()
        {
            if (isPlayerLocked)
                return;
            SendDirection(desiredDirection);
        }
    }
}