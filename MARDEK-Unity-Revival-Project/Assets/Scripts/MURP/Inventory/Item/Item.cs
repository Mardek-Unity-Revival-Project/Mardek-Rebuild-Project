using UnityEngine;
using MURP.Core;
using MURP.StatsSystem;

namespace MURP.InventorySystem
{
    [CreateAssetMenu(menuName = "MURP/Inventory/Item")]
    public class Item : AddressableScriptableObject
    {
        [SerializeField] string _displayName;
        [SerializeField] string _description;
        [SerializeField] Sprite _sprite;
        [SerializeField] Element _element;
        [SerializeField] int _price;

        public string displayName { get { return _displayName; } }

        public string description { get { return CreateFullDescription(_description); } }

        public string properties { get { return CreateProperties(); } }

        public Sprite sprite { get { return _sprite; } }

        public Element element { get { return _element; } }

        public int price { get { return _price; } }

        public virtual bool CanStack()
        {
            return true;
        }

        protected virtual string CreateFullDescription(string rawDescription)
        {
            return "MISCELLANIOUS ITEM\n\n\n" + rawDescription;
        }

        protected virtual string CreateProperties()
        {
            return "";
        }
    }
}