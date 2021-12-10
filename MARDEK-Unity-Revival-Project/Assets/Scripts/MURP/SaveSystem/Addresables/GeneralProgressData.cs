using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MURP.CharacterSystem;

namespace MURP.SaveSystem
{
    public class GeneralProgressData : AddressableMonoBehaviour
    {
        [SerializeField, HideInInspector] string _gameName = string.Empty;
        [SerializeField, HideInInspector] string currentScene = default;
        [SerializeField] List<Character> characters;

        public string GameName
        {
            get
            {
                return _gameName;
            }
            set
            {
                if (string.IsNullOrEmpty(_gameName))
                    _gameName = value;
                return;
            }
        }
        public override void Save()
        {
            currentScene = SceneManager.GetActiveScene().path;
            base.Save();
        }
        public void LoadScene()
        {
            if (string.IsNullOrEmpty(currentScene))
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            else
                SceneManager.LoadScene(currentScene);
        }
    }
}