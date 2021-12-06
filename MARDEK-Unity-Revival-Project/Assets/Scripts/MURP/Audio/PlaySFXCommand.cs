using UnityEngine;

namespace MURP.Audio
{
    public class PlaySFXCommand : CommandBase
    {
        [SerializeField] SoundEffect sound;

        public override void Trigger()
        {
            AudioManager.PlaySoundEffect(sound);
        }
    }
}