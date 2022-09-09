using MURP.StatsSystem;
using UnityEngine;

namespace MURP.DiscoverySystem
{
    [CreateAssetMenu(menuName = "MURP/Encyclopedia/Artefact")]
    public class EncyclopediaArtefact : EncyclopediaItem
    {
        [SerializeField] Sprite _image;
        [SerializeField] string _description;

        public Sprite image { get { return _image; } }
        public string description { get { return _description; } }
    }
}
