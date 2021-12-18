using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MURP.SaveSystem
{
    public class GeneralProgressData : AddressableMonoBehaviour
    {
        [SerializeField] string currentScene = default;
        [SerializeField] string _gameName = string.Empty;
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