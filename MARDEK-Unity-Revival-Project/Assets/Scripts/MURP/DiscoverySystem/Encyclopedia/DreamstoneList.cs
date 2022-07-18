using System.Collections.Generic;
using UnityEngine;

namespace MURP.DiscoverySystem
{
    [CreateAssetMenu(menuName = "MURP/Encyclopedia/DreamstoneList")]
    public class DreamstoneList : ScriptableObject
    {
        [SerializeField] List<Dreamstone> _dreamstones;

        public List<Dreamstone> dreamstones { get { return _dreamstones; } }
    }
}
