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
        [SerializeField] SpriteAnimator animator = null;

        
        public bool isMoving { get; private set; }
        MoveDirection currentDirection = null;
        Vector2 targetPosition = Vector2.zero;
        Queue<MoveDirection> queuedMoves = new Queue<MoveDirection>();

        new Collider2D collider = null;
        ContactFilter2D filter = default;
        
        private void Awake()
        {
            targetPosition = transform.position;
            collider = GetComponent<Collider2D>();
            if (collider)
            {
                filter.useLayerMask = true;
                LayerMask mask = Physics2D.GetLayerCollisionMask(gameObject.layer);
                filter.layerMask = mask;
            }
        }

        public void EnqueueMoves(List<MoveDirection> directions)
        {
            foreach (MoveDirection direction in directions)
                queuedMoves.Enqueue(direction);
            TryToStartMove();
        }

        public void Move(MoveDirection direction)
        {
            if (isMoving)
                return;
            queuedMoves = new Queue<MoveDirection>();
            queuedMoves.Enqueue(direction);
            TryToStartMove();
        }

        private void FixedUpdate()
        {
            if (isMoving)
            {
                bool previousIsMoving = isMoving;
                isMoving = !MoveToFixed(transform, targetPosition, movementSpeed);
                TryToStartMove();
            }
        }

        void TryToStartMove()
        {
            // wont call get next if isMoving is true
            bool hasNextMove = (isMoving == false) && GetNextTargetPosition();
            MoveColliderToPosition(targetPosition);

            if (ColliderOverlaps() == false)
            {
                if (hasNextMove)
                {
                    isMoving = true; 
                    UpdateAnimator();
                }
            }
            else
                MoveColliderToPosition(transform.position);

            if (isMoving == false)
                UpdateAnimator();

        }

        bool GetNextTargetPosition()
        {
            if(queuedMoves.Count > 0)
            {
                currentDirection = queuedMoves.Dequeue();
                targetPosition = (Vector2)transform.position + currentDirection.value;
                return true;
            }
            return false;
        }

        private void UpdateAnimator()
        {
            if (animator == null)
                return;
            animator.ChangeClipByReferecen(currentDirection);
            if(!isMoving)
                animator.ChangeClipByReferecen(null);
        }

        private void MoveColliderToPosition(Vector2 position)
        {
            if (collider)
                collider.offset = position - (Vector2)transform.position;
        }

        private bool ColliderOverlaps()
        {
            if (collider == null)
                return false;
            Collider2D[] results = new Collider2D[64];
            int n = collider.OverlapCollider(filter, results);
            return (n > 0);
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
