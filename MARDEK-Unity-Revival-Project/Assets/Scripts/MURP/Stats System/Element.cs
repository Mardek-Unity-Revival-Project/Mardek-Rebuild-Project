using UnityEngine;

namespace MURP.StatsSystem
{
    [CreateAssetMenu(menuName = "MURP/StatsSystem/Element")]
    public class Element : ScriptableObject
    {
        [SerializeField] Sprite _thinSprite;
        [SerializeField] Sprite _thickSprite;

        public Sprite thinSprite { get { return _thinSprite; } }

        public Sprite thickSprite { get { return _thickSprite; } }
    }
}