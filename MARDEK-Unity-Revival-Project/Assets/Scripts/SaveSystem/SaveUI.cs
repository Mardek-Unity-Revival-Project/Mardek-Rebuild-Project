using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveUI : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text text = null;

    private void Update()
    {
        UpdateUI();
    }

    [ContextMenu("Save quicksave")]
    public void SaveFile()
    {
        SaveSystem.SaveToFile();
    }
    [ContextMenu("Load quicksave")]
    public void LoadFile()
    {
        SaveSystem.LoadFromFile();
    }

    public void UpdateUI()
    {
        text.text = SaveSystem.PrintObjectMap();
    }
}
