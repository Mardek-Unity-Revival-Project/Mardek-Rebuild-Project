using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace JRPG
{
    public class PlayerMovementController : MovementController
    {
        Vector2 desiredDirection = Vector2.zero;

        public void OnMovementInput(InputAction.CallbackContext ctx)
        {
            Vector2 direction = ctx.ReadValue<Vector2>();
            if (direction.x == 0 || direction.y == 0)
                desiredDirection = direction.normalized;
        }

        //public void OnDownInput()
        //{
        //    desiredDirection = Vector2.down;
        //}
        //public void OnLeftInput()
        //{
        //    desiredDirection = Vector2.left;
        //}
        //public void OnRightInput()
        //{
        //    desiredDirection = Vector2.right;
        //}
        //public void OnUpInput()
        //{
        //    desiredDirection = Vector2.up;
        //}

        private void Update()
        {
            if(desiredDirection != Vector2.zero)
            {
                controlledMovement.Move(desiredDirection);
                //desiredDirection = Vector2.zero;
            }
        }
    }
}
