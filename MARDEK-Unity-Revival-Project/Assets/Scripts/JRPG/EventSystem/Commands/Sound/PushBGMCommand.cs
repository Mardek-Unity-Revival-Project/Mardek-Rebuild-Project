using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JRPG
{
    public class PushBGMCommand : CommandBase
    {

        [SerializeField]
        AudioClip music;

        public override void Trigger()
        {
            SoundManager.PushBackgroundMusic(music);
        }
    }
}
