using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using JRPG;
using System.Threading;

public class ProgressData : AddressableMB
{
    [SerializeField] string currentScene = default;
    [SerializeField] List<Vector2> inMapPartyPositions = new List<Vector2>();

    public override void Save()
    {
        currentScene = SceneManager.GetActiveScene().path;
        inMapPartyPositions = InMapParty.GetPartyPosition();
        base.Save();
    }

    [ContextMenu("RebuildLoadedScene")]
    void RebuildLoadedScene()
    {
        InMapParty.positionsToLoad = inMapPartyPositions;
        SceneManager.LoadScene(currentScene);
    }
}