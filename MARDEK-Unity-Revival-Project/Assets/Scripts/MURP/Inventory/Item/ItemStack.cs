using UnityEngine;

namespace MURP.Inventory
{
    public class ItemStack : MonoBehaviour
    {
        [SerializeField] Item _item;
        [SerializeField] int _amount;

        public ItemStack(Item item, int amount) {
            this._item = item;
            this._amount = amount;
            this.Validate();
        }

        void Init()
        {
            this.Validate();
        }

        void Validate()
        {
            if (this.item == null)
            {
                throw new System.ArgumentException("Item can't be null");
            }
            if (this.amount <= 0)
            {
                throw new System.ArgumentException("Amount (" + this.amount + ") must be positive");
            }
        }

        public Item item { get { return _item; } }

        public int amount { get { return _amount; } }
    }
}