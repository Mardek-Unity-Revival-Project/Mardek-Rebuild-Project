using UnityEngine;
using System.Collections;

namespace JRPG {
    [RequireComponent(typeof(GridObject))]
    public class Waypoint : MonoBehaviour
    {
        [SerializeField] WaypointEnum thisWaypoint = null;

        private void Awake()
        {
            if (Transition.usedWaypoint)
            {
                if (thisWaypoint == Transition.usedWaypoint)
                {
                    InMapParty.PositionPartyAt(transform.position, Transition.transitionFacingDirection);
                }
            }
        }
    } 
}