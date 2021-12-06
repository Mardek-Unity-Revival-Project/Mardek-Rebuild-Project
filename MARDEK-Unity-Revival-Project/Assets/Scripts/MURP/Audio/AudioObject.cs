using UnityEngine;

namespace MURP.Audio
{
    public abstract class AudioObject : ScriptableObject
    {
        [SerializeField] protected AudioClip clip = default;

        public abstract void PlayOnSource(AudioSource audioSource);
    }
}