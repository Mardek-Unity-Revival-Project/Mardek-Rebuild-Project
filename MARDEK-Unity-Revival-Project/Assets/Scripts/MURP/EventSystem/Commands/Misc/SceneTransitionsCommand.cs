using UnityEngine;
using UnityEngine.SceneManagement;

namespace MURP.EventSystem
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

            SetFacingDirection();

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