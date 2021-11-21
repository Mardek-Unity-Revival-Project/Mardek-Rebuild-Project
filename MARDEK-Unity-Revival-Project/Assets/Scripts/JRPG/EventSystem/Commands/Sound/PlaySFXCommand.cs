using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JRPG
{
    public class PlaySFXCommand : CommandBase
    {

        [SerializeField]
        SoundEffect sound;

        public override void Trigger()
        {
            AudioManager.PlaySoundEffect(sound);
        }
    }
}
