﻿using UnityEngine;
using System.Collections.Generic;

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
                    //Debug.Log($"Arriving at waypoint {thisWaypoint}");
                    var pos = new List<Vector2>();
                    pos.Add(transform.position);
                    InMapParty.positionsToLoad = pos;
                    //InMapParty.PositionPartyAt(pos, TransitionCommand.transitionFacingDirection);
                    TransitionCommand.ClearFacingDirection();
                }
            }
        }
    } 
}