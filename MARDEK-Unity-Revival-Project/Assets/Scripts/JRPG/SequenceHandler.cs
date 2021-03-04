using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JRPG
{
    public class SequenceHandler : MonoBehaviour
    {
        [SerializeField] Sequence sequence = null;

        public void Trigger()
        {
            sequence.Trigger();
        }
    }
}
