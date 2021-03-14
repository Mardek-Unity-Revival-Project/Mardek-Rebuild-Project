using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace JRPG
{
    public class Transition : CommandBase
    {
        [SerializeField] SceneReference scene = null;
        [SerializeField] WaypointEnum waypoint = null;
        [SerializeField] MoveDirection overrideFacingDirection = null;

        public static WaypointEnum usedWaypoint { get; private set; }
        public static MoveDirection transitionFacingDirection { get; private set; }

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
    }
}
