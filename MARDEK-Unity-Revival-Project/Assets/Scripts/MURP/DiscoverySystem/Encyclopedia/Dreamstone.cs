using System.Collections.Generic;
using UnityEngine;

namespace MURP.DiscoverySystem
{
    [CreateAssetMenu(menuName = "MURP/Encyclopedia/Dreamstone")]
    public class Dreamstone : ScriptableObject
    {
        [SerializeField] List<DreamstoneMessage> _messages;
        public bool isNew;

        public List<DreamstoneMessage> messages { get { return _messages; } }
    }
}
