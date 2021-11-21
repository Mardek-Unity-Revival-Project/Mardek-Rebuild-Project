using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JRPG
{
    public class PlaySFXCommand : CommandBase
    {

        [SerializeField]
        AudioObject sound;

        public override void Trigger()
        {
            AudioManager.PlaySoundEffect(sound);
        }
    }
}
