using UnityEngine;

namespace MURP.DiscoverySystem
{
    public abstract class EncyclopediaItem : ScriptableObject
    {
        [SerializeField] Sprite _icon;
        [SerializeField] string _displayName;
        public bool isDiscovered;

        public Sprite icon { get { return _icon; } }
        public string displayName { get { return _displayName; } }
    }
}
