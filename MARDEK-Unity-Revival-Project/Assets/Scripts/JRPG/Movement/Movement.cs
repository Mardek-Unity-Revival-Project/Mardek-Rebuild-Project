using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JRPG
{
    [RequireComponent(typeof(GridObject))]
    public class Movement : MonoBehaviour
    {
        [SerializeField] float movementSpeed = 1f;
        [SerializeField] SpriteAnimator animator = null;

        public bool isMoving { get; private set; }
        Vector2 targetPosition = Vector2.zero;
        Queue<MoveDirection> queuedMoves = new Queue<MoveDirection>();

        public void EnqueueMoves(List<MoveDirection> directions)
        {
            foreach (MoveDirection direction in directions)
                queuedMoves.Enqueue(direction);
            UpdateMoveState();
        }

        public void Move(MoveDirection direction)
        {
            if (isMoving)
                return;
            queuedMoves = new Queue<MoveDirection>();
            queuedMoves.Enqueue(direction);
            UpdateMoveState();
        }

        private void FixedUpdate()
        {
            if (isMoving)
            {
                isMoving = !MoveToFixed(transform, targetPosition, movementSpeed);
                UpdateMoveState();
            }
        }

        void UpdateMoveState()
        {
            if (isMoving == false)
            {
                if (queuedMoves.Count > 0)
                {
                    //start new movement
                    MoveDirection newDirection = queuedMoves.Dequeue();
                    targetPosition = (Vector2)transform.position + newDirection.value;
                    isMoving = true;
                    if (animator)
                        animator.ChangeClipByReferecen(newDirection);
                }
                if (isMoving == false)
                {
                    //stoped moving
                    if (animator)
                        animator.ChangeClipByReferecen(null);
                }
            }
        }

        bool MoveToFixed(Transform transform, Vector2 targetPosition, float movementSpeed)
        {
            Vector2 positionDifferece = new Vector2(targetPosition.x, targetPosition.y) - (Vector2)transform.position;
            if (positionDifferece == Vector2.zero)
            {
                return true;
            }
            Vector2 increment = positionDifferece.normalized * Time.fixedDeltaTime * movementSpeed;
            if (increment.sqrMagnitude < positionDifferece.sqrMagnitude)
            {
                transform.position = ((Vector2)transform.position + increment);
                return false;
            }
            else
            {
                //end movement
                transform.position = (targetPosition);
                return true;
            }
        }

    }
}
