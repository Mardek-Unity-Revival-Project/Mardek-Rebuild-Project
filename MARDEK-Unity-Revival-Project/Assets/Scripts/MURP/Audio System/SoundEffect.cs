using UnityEngine;

namespace MURP.Audio
{
    [CreateAssetMenu(menuName = "MURP/Audio/SoundEffect")]
    public class SoundEffect : AudioObject
    {
        public override void PlayOnSource(AudioSource audioSource)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}