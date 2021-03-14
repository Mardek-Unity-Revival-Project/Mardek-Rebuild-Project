using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JRPG
{
    public class FollowMovementController : MovementController
    {
        [SerializeField] Movement followedMovement;
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
                if (Vector2.Distance(followedMovement.lastPosition, transform.position) < Vector2.kEpsilonNormalSqrt)
                    shouldFollow = false;
                else
                    MoveToFollowed();
            }
        }
    }
}