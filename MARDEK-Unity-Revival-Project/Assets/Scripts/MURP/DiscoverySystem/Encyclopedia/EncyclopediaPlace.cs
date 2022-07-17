using MURP.StatsSystem;
using UnityEngine;

namespace MURP.DiscoverySystem
{
    [CreateAssetMenu(menuName = "MURP/Encyclopedia/Place")]
    public class EncyclopediaPlace : EncyclopediaItem
    {
        [SerializeField] Sprite _landscape;
        [SerializeField] string _description;

        public Sprite landscape { get { return _landscape; } }
        public string description { get { return _description; } }
    }
}
