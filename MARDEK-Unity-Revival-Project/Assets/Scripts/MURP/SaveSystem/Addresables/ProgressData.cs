using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MURP.Movement;

namespace MURP
{
    public class ProgressData : AddressableMonoBehaviour
    {
        [SerializeField] string currentScene = default;
        [SerializeField] List<Vector2> inMapPartyPositions = new List<Vector2>();
        [SerializeField] List<MoveDirection> inMapPartyDirections = new List<MoveDirection>();

        public override void Save()
        {
            currentScene = SceneManager.GetActiveScene().path;
            inMapPartyPositions = InMapParty.GetPartyPosition();
            inMapPartyDirections = InMapParty.GetPartyDirections();
            base.Save();
        }

        [ContextMenu("RebuildLoadedScene")]
        public void RebuildLoadedScene()
        {
            InMapParty.positionsToLoad = inMapPartyPositions;
            InMapParty.directionsToLoad = inMapPartyDirections;
            SceneManager.LoadScene(currentScene);
        }
    }
}