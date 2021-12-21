using UnityEngine;
using UnityEngine.SceneManagement;
using MURP.Core;
using MURP.EventSystem;
using MURP.SaveSystem;

namespace MURP.MovementSystem
{
    public class SceneTransitionCommand : CommandBase
    {
        public static WaypointEnum usedWaypoint { get; private set; }
        public static MoveDirection transitionFacingDirection { get; private set; }

        [SerializeField] SceneReference scene = null;
        [SerializeField] WaypointEnum waypoint = null;
        [SerializeField] MoveDirection overrideFacingDirection = null;

        public override void Trigger()
        {
            usedWaypoint = waypoint;

            if(overrideFacingDirection)
                SetFacingDirection();

            //Command queue won't have the oportunity to reset the lockValue itself cause the scene reload will destroy the object
            PlayerLocks.EventSystemLock = 0;
            AddressableMonoBehaviour.SaveOnTransition();
            SceneManager.LoadScene(scene);
        }

        void SetFacingDirection()
        {
            if (overrideFacingDirection)
                transitionFacingDirection = overrideFacingDirection;
            else
                transitionFacingDirection = PlayerController.GetPlayerMovement().currentDirection;
        }

        public static void ClearUsedWaypoint()
        {
            usedWaypoint = null;
        }
    }
}