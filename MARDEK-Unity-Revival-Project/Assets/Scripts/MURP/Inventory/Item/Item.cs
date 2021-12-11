using UnityEngine;

namespace MURP.Inventory
{
    [CreateAssetMenu(menuName = "MURP/Inventory/Item")]
    public class Item : ScriptableObject
    {
        [SerializeField] string _displayName;
        [SerializeField] string _description;

        // TODO _element (but can only be done after we have some kind of Element enum)
        [SerializeField] int _price;

        public string displayName { get { return _displayName; } }

        public string description { get { return _description; } }

        public int price { get { return _price; } }
    }
}