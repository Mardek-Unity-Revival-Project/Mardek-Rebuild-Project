using UnityEngine;
using System.Collections;

namespace JRPG {
    [RequireComponent(typeof(GridObject))]
    public class Waypoint : MonoBehaviour
    {
        [SerializeField] WaypointEnum thisWaypoint = null;

        private void Start()
        {
            if (thisWaypoint)
                if (thisWaypoint == Transition.usedWaypoint)
                    Debug.Log(transform.position);
        }
    }    
}
