using UnityEngine;
using System.Collections.Generic;
using MURP.EventSystem;

namespace MURP.Movement
{
    [RequireComponent(typeof(GridObject))]
    public class Waypoint : MonoBehaviour
    {
        [SerializeField] WaypointEnum thisWaypoint = null;

        private void Awake()
        {
            if (SceneTransitionCommand.usedWaypoint != null)
            {
                if (thisWaypoint == SceneTransitionCommand.usedWaypoint)
                {
                    var pos = new List<Vector2>();
                    pos.Add(transform.position);
                    InMapParty.positionsToLoad = pos;
                    InMapParty.directionsToLoad = new List<MoveDirection>(){ SceneTransitionCommand.transitionFacingDirection};
                    SceneTransitionCommand.ClearUsedWaypoint();
                }
            }
        }
    } 
}