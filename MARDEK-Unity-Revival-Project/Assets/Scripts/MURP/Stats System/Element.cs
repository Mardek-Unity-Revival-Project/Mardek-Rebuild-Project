using UnityEngine;

namespace MURP.StatsSystem
{
    [CreateAssetMenu(menuName = "MURP/StatsSystem/Element")]
    public class Element : ScriptableObject
    {
        [SerializeField] Sprite _sprite;

        public Sprite sprite { get { return _sprite; } }
    }
}