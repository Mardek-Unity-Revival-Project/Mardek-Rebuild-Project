using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MURP.Movement;

namespace MURP.SaveSystem
{
    public class InMapProgressData : AddressableMonoBehaviour
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
            if (string.IsNullOrEmpty(currentScene))
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            else
                SceneManager.LoadScene(currentScene);
        }
    }
}