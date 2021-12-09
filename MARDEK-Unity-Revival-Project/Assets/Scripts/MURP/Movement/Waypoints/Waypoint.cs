using UnityEngine;
using System.Collections.Generic;
using MURP.Core;

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
                    var directions = new List<MoveDirection>() { SceneTransitionCommand.transitionFacingDirection };
                    InMapParty.SetPositionAndDirectionOverrides(pos, directions);
                    SceneTransitionCommand.ClearUsedWaypoint();
                }
            }
        }
    } 
}