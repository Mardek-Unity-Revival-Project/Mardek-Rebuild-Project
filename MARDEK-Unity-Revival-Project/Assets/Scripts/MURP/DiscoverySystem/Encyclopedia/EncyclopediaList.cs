using System.Collections.Generic;
using UnityEngine;

namespace MURP.DiscoverySystem
{
    [CreateAssetMenu(menuName = "MURP/Encyclopedia/List")]
    public class EncyclopediaList : ScriptableObject
    {
        [SerializeField] List<EncyclopediaItem> _items;

        public List<EncyclopediaItem> items { get { return _items; } }
    }
}
