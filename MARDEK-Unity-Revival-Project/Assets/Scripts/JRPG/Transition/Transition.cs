using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace JRPG
{
    public class Transition : CommandBase
    {
        [SerializeField] SceneReference scene = null;
        [SerializeField] WaypointEnum waypoint = null;

        public static WaypointEnum usedWaypoint;

        public override void Trigger()
        {
            usedWaypoint = waypoint;
            SceneManager.LoadScene(scene);
        }
    }
}
