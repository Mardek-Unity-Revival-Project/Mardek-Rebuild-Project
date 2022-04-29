using UnityEngine;

namespace MURP.Audio
{
    [CreateAssetMenu(menuName = "MURP/Audio/Music")]
    public class Music : AudioObject
    {
        [SerializeField] string _displayName;
        public string displayName { get { return _displayName; } }

        [SerializeField] int _id;
        public int id { get { return _id; } }

        public override void PlayOnSource(AudioSource audioSource)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }

        public AudioClip GetClip()
        {
            return clip;
        }
    }
}