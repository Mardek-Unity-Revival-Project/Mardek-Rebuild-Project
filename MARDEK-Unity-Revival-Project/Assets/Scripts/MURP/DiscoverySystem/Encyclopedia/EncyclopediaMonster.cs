using MURP.StatsSystem;
using UnityEngine;

namespace MURP.DiscoverySystem
{
    [CreateAssetMenu(menuName = "MURP/Encyclopedia/Monster")]
    public class EncyclopediaMonster : EncyclopediaItem
    {
        [SerializeField] Element _element;
        [SerializeField] string _description;
        [SerializeField] Sprite _portrait;
        [SerializeField] string _battleClass;
        [SerializeField] string _battleType;

        // TODO This should probably be linked to some kind of general battle info data structure

        public Element element { get { return _element; } }
        public string description { get { return _description; } }
        public Sprite portrait { get { return _portrait; } }
        public string battleClass { get { return _battleClass; } }
        public string battleType { get { return _battleType; } }
    }
}
