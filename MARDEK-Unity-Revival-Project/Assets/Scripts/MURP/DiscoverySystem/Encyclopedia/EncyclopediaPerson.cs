using MURP.StatsSystem;
using UnityEngine;

namespace MURP.DiscoverySystem
{
    [CreateAssetMenu(menuName = "MURP/Encyclopedia/Person")]
    public class EncyclopediaPerson : EncyclopediaItem
    {
        [SerializeField] Sprite _portrait;
        [SerializeField] string _fullName;
        [SerializeField] string _fullDescription;
        [SerializeField] string _race;
        [SerializeField] string _gender;
        [SerializeField] int _age;
        [SerializeField] string _battleClass;
        [SerializeField] Element _element;
        [SerializeField] string _placeOfOrigin;
        [SerializeField] string _weapon;
        [SerializeField] string _alignment;

        public Sprite portrait { get { return _portrait; } }
        public string fullName { get { return _fullName; } }
        public string fullDescription { get { return _fullDescription; } }
        public string race { get { return _race; } }
        public string gender { get { return _gender; } }
        public int age { get { return _age; } }
        public string battleClass { get { return _battleClass; } }
        public Element element { get { return _element; } }
        public string placeOfOrigin { get { return _placeOfOrigin; } }
        public string weapon { get { return _weapon; } }
        public string alignment { get { return _alignment; } }
    }
}
