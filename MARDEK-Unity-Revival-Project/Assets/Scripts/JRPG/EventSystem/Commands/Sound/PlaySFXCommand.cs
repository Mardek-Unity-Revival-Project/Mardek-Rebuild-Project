using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JRPG
{
    public class PlaySFXCommand : CommandBase
    {

        [SerializeField]
        AudioClip sound;

        public override void Trigger()
        {
            SoundManager.PlaySoundEffect(sound);
        }
    }
}
