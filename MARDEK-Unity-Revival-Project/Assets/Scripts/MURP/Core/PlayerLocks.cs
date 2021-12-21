using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MURP.Core
{
    public class PlayerLocks
    {
        public static int EventSystemLock { get; set; }
        public static int UISystemLock { get; set; }
        public static bool isPlayerLocked
        {
            get
            {
                return EventSystemLock > 0 || UISystemLock > 0;
            }
        }
    }
}
