using UnityEngine;

namespace MURP.UI
{
    public class UIUtils : MonoBehaviour
    {
        public void QuitGame()
        {
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}