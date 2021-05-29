using UnityEngine;
using System.Collections;

namespace JRPG {
    [RequireComponent(typeof(GridObject))]
    public class Waypoint : MonoBehaviour
    {
        [SerializeField] WaypointEnum thisWaypoint = null;

        private void Awake()
        {
            if (TransitionCommand.usedWaypoint)
            {
                if (thisWaypoint == TransitionCommand.usedWaypoint)
                {
                    InMapParty.PositionPartyAt(transform.position, TransitionCommand.transitionFacingDirection);
                }
            }
        }
    } 
}