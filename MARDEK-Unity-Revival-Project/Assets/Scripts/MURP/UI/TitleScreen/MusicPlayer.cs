using MURP.Audio;
using UnityEngine;

namespace MURP.UI
{
    public class MusicPlayer : MonoBehaviour
    {
        [SerializeField] Music emptyMusic;

        bool isPlayingMusic;

        public void GoBack()
        {
            // If we are currently playing something, stop
            if (isPlayingMusic)
            {
                AudioManager.PopMusic();
                isPlayingMusic = false;
            }

            // Pop the 'no music' entry from the music stack
            AudioManager.PopMusic();
            gameObject.SetActive(false);
        }

        void OnEnable()
        {
            AudioManager.PushMusic(emptyMusic);
        }

        public void PlaySelectedMusic()
        {
            if (isPlayingMusic)
            {
                AudioManager.PopMusic();
            }
            AudioManager.PushMusic(MusicEntry.selectedMusic);
            isPlayingMusic = true;
        }
    }
}
