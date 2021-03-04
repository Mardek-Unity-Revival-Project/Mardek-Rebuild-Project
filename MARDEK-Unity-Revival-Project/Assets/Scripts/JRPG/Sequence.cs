using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JRPG { 
    [CreateAssetMenu(menuName = "JRPG/Sequence")]
    public class Sequence : ScriptableObject
    {
        [SerializeField] List<Sequence> events = new List<Sequence>();

        public virtual void Trigger()
        {
            foreach (Sequence s in events)
            {
                s.Trigger();
            }
        }
    }
}
