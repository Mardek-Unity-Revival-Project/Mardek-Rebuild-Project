using UnityEngine;

namespace MURP.Audio
{
    [CreateAssetMenu(menuName = "Audio/Music")]
    public class Music : AudioObject
    {
        public override void PlayOnSource(AudioSource audioSource)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}