using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProgressObject : AddressableMB
{
    [SerializeField] string currentMap = default;

    private void Awake()
    {
        currentMap = SceneManager.GetActiveScene().path;
        Save();
    }
}
