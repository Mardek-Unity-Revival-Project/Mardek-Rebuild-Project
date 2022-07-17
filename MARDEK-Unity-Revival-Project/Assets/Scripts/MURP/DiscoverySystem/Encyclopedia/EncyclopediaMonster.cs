using MURP.StatsSystem;
using UnityEngine;

namespace MURP.DiscoverySystem
{
    [CreateAssetMenu(menuName = "MURP/Encyclopedia/Monster")]
    public class EncyclopediaMonster : EncyclopediaItem
    {
        [SerializeField] Sprite _portrait;
        [SerializeField] string _battleClass;
        [SerializeField] string _battleType;

        // TODO This should probably be linked to some kind of general battle info data structure
    }
}
