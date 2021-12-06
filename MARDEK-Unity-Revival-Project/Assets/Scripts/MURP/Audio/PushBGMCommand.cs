using UnityEngine;

namespace MURP.Audio
{
    public class PushBGMCommand : CommandBase
    {
        [SerializeField] Music music;

        public override void Trigger()
        {
            AudioManager.PushMusic(music);
        }
    }
}