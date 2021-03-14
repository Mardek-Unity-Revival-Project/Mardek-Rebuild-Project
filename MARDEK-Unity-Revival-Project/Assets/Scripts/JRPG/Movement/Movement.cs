using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JRPG
{
    //[RequireComponent(typeof(GridObject))]
    public class Movement : MonoBehaviour
    {
        [SerializeField] float movementSpeed = 1f;

        [SerializeField] ColliderHelper _colliderHelper = null;
        [SerializeField] SpriteAnimator animator = null;

        public ColliderHelper colliderHelper { get { return _colliderHelper; } }
        public bool isMoving { get; private set; }
        public MoveDirection currentDirection { get; private set; }
        public Vector2 lastPosition
        {
            get
            {
                if (currentDirection) 
                    return targetPosition - currentDirection.value;
                else 
                    return transform.position;
            }
        }
        Vector2 targetPosition = Vector2.zero;
        Queue<MoveDirection> queuedMoves = new Queue<MoveDirection>();

        public delegate void MoveDelegate();

        public event MoveDelegate OnMove = delegate { };

        private void Awake()
        {
            targetPosition = transform.position;            
        }

        public void EnqueueMoves(List<MoveDirection> directions)
        {
            foreach (MoveDirection direction in directions)
                queuedMoves.Enqueue(direction);
            UpdateMoveStatus();
        }

        public void MoveInDirectionOnce(MoveDirection direction)
        {
            if (isMoving)
                return;
            queuedMoves.Clear();
            if(direction != null)
            {
                queuedMoves.Enqueue(direction);
                UpdateMoveStatus();
            }
        }

        public void FaceDirection(MoveDirection direction)
        {
            currentDirection = direction;
            UpdateAnimatorWithCurrentDirection();
            StopAnimator();
        }

        private void LateUpdate()
        {
            if (isMoving)
            {
                isMoving = !MoveToPosition(transform, targetPosition, movementSpeed, Time.deltaTime);
                UpdateMoveStatus();
            }
        }

        void UpdateMoveStatus()
        {            
            if (isMoving == false)
            {
                bool shouldMove = ShouldMove();
                if (shouldMove)
                {
                    isMoving = true;
                    OnMove.Invoke();
                }
                else
                {
                    if (colliderHelper) colliderHelper.OffsetCollider(Vector2.zero);
                    StopAnimator();
                }
            }
            else
                if(colliderHelper)
                    colliderHelper.OffsetCollider(targetPosition - (Vector2)transform.position);
        }

        bool ShouldMove()
        {
            Vector2 previousTargetPosition = targetPosition;
            bool hasNextMove = GetNextTargetPosition();
            if (hasNextMove)
            {
                UpdateAnimatorWithCurrentDirection();
                if (colliderHelper == null)
                    return true;
                colliderHelper.OffsetCollider(targetPosition - (Vector2)transform.position);
                if (colliderHelper.Overlaping().Count == 0)
                {
                    return true;
                }
            }
            targetPosition = previousTargetPosition;
            return false;
        }

        bool GetNextTargetPosition()
        {
            if(queuedMoves.Count > 0)
            {
                currentDirection = queuedMoves.Dequeue();
                if (currentDirection == null)
                    return false;
                targetPosition = (Vector2)transform.position + currentDirection.value;
                return true;
            }
            return false;
        }

        private void UpdateAnimatorWithCurrentDirection()
        {
            //Debug.Log("play");
            if (animator) animator.PlayClipByMoveDirectionReference(currentDirection);
        }

        void StopAnimator()
        {
            //Debug.Log("stop");
            if (animator) animator.StopCurrentAnimation(1); //end with last sprite
        }

        bool MoveToPosition(Transform transform, Vector2 targetPosition, float movementSpeed, float deltaTime)
        {
            Vector2 positionDifferece = new Vector2(targetPosition.x, targetPosition.y) - (Vector2)transform.position;
            if (positionDifferece == Vector2.zero)
            {
                return true;
            }
            Vector2 increment = positionDifferece.normalized * deltaTime * movementSpeed;
            if (increment.sqrMagnitude < positionDifferece.sqrMagnitude)
            {
                Utilities2D.SetTransformPosition(transform, ((Vector2)transform.position + increment));
                return false;
            }
            else
            {
                //end movement
                Utilities2D.SetTransformPosition(transform, targetPosition);
                return true;
            }
        }

        

    }
}
