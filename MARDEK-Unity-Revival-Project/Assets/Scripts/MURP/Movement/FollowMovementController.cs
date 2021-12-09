using UnityEngine;
using MURP.Core;

namespace MURP.Movement
{
    public class FollowMovementController : MovementController
    {
        [SerializeField] Movable followedMovement;
        bool shouldFollow = false;

        private void OnEnable()
        {
            if (followedMovement)
                followedMovement.OnMove += OnFollowedMovementMoved;
        }

        private void OnDisable()
        {
            if (followedMovement)
                followedMovement.OnMove -= OnFollowedMovementMoved;
        }

        void OnFollowedMovementMoved()
        {
            shouldFollow = true;
            MoveToFollowed();
        }

        void MoveToFollowed()
        {
            Vector2 desiredDelta = followedMovement.lastPosition - (Vector2)transform.position;
            MoveDirection followDirection = ApproximanteDirectionByVector2(desiredDelta);
            if (followDirection)
            {
                SendDirection(followDirection);
            }
        }

        private void Update()
        {
            if (shouldFollow)
            {
                if (Vector2.Distance(followedMovement.lastPosition, transform.position) < 2 * Vector2.kEpsilon)
                    shouldFollow = false;
                else
                    MoveToFollowed();
            }
        }
    }
}