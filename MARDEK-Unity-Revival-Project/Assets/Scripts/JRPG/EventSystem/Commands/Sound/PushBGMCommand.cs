using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JRPG
{
    public class PushBGMCommand : CommandBase
    {

        [SerializeField]
        Music music;

        public override void Trigger()
        {
            AudioManager.PushMusic(music);
        }
    }
}
