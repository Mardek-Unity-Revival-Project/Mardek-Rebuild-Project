using UnityEngine;

namespace MURP.Audio
{
    [CreateAssetMenu(menuName = "MURP/Audio/Music")]
    public class Music : AudioObject
    {
        public override void PlayOnSource(AudioSource audioSource)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}