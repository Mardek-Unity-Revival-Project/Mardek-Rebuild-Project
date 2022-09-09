using MURP.Audio;
using UnityEngine;
using UnityEngine.UI;

namespace MURP.UI
{
    public class MusicPlayer : MonoBehaviour
    {
        [SerializeField] Music emptyMusic;
        [SerializeField] Text currentPlayTimeText;
        [SerializeField] Text currentMusicText;
        [SerializeField] Image progressBar;
        [SerializeField] Button pausePlayButton;
        [SerializeField] Image playIcon;
        [SerializeField] Image pauseIcon;

        Music playingMusic = null;
        bool isPaused;

        void UpdatePausePlayUI()
        {
            if (playingMusic != null)
            {
                float rawCurrentPlayTime = AudioManager.GetMusicAudioSource().time;
                int currentPlayTime = (int) rawCurrentPlayTime;
                int currentPlaySeconds = currentPlayTime % 60;
                float currentMusicDuration = playingMusic.GetClip().length;

                currentMusicText.text = playingMusic.displayName.ToUpper();
                currentPlayTimeText.text = (currentPlayTime / 60) + ":" + (currentPlaySeconds >= 10 ? "" : "0") + currentPlaySeconds;
                progressBar.transform.localScale = new Vector3(rawCurrentPlayTime / currentMusicDuration, 1f, 1f);
                pausePlayButton.interactable = true;
                if (isPaused)
                {
                    playIcon.gameObject.SetActive(true);
                    pauseIcon.gameObject.SetActive(false);
                }
                else
                {
                    playIcon.gameObject.SetActive(false);
                    pauseIcon.gameObject.SetActive(true);
                    pauseIcon.color = new Color(1f, 1f, 1f, 1f);
                }
            }
            else
            {
                currentMusicText.text = "";
                currentPlayTimeText.text = "0:00";
                progressBar.transform.localScale = new Vector3(0f, 1f, 1f);
                pausePlayButton.interactable = false;
                playIcon.gameObject.SetActive(false);
                pauseIcon.gameObject.SetActive(true);
                pauseIcon.color = new Color(1f, 1f, 1f, 0.3f);
            }
        }

        void FixedUpdate()
        {
            UpdatePausePlayUI();
        }

        public void GoBack()
        {
            // If we are currently playing something, stop
            if (playingMusic != null)
            {
                AudioManager.PopMusic();
                playingMusic = null;
            }
            if (isPaused)
            {
                AudioManager.GetMusicAudioSource().UnPause();
                isPaused = false;
            }

            // Pop the 'no music' entry from the music stack
            AudioManager.PopMusic();
            gameObject.SetActive(false);
        }

        void OnEnable()
        {
            AudioManager.PushMusic(emptyMusic);
            UpdatePausePlayUI();
        }

        public void PlaySelectedMusic()
        {
            if (playingMusic != null)
            {
                AudioManager.PopMusic();
            }
            if (isPaused)
            {
                AudioManager.GetMusicAudioSource().UnPause();
                isPaused = false;
            }
            AudioManager.PushMusic(MusicEntry.selectedMusic);
            
            playingMusic = MusicEntry.selectedMusic;
            UpdatePausePlayUI();
        }

        public void PauseOrPlay()
        {
            if (isPaused)
            {
                AudioManager.GetMusicAudioSource().UnPause();
                isPaused = false;
            }
            else
            {
                AudioManager.GetMusicAudioSource().Pause();
                isPaused = true;
            }
            UpdatePausePlayUI();
        }
    }
}
