using UnityEngine;
using UnityEngine.SceneManagement;

namespace MURP.SaveSystem
{
    public class GeneralProgressData : AddressableMonoBehaviour
    {
        [SerializeField, HideInInspector] string _gameName = string.Empty;
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
        [SerializeField, HideInInspector] string currentScene = default;

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
