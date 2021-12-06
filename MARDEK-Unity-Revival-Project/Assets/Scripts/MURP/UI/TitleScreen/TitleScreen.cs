using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using MURP.SaveSystem;

namespace MURP.UI
{
    public class TitleScreen : MonoBehaviour
    {
        [SerializeField] GeneralProgressData progressData;
        [SerializeField] InputField gameName;
        [SerializeField] UnityEvent startGame;

        public void TryStartNewGame()
        {
            if (ValidateName(gameName.text))
            {
                progressData.GameName = gameName.text;
                startGame.Invoke();
            }
        }

        bool ValidateName(string name)
        {
            return !string.IsNullOrEmpty(name);
        }
    }
}
