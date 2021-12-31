using UnityEngine;
using System.Collections.Generic;

namespace MURP.Inventory
{
    [System.Serializable]
    public class Slot
    {
        [SerializeField] Item currentItem;
        [SerializeField] int currentAmount;
        /*
         * If this is empty, any item can be placed in this slot. If this is non-empty, only equippable items whose category is
         * included in this list can be placed in this slot.
         */
        [SerializeField] List<EquipmentCategory> itemFilter = new List<EquipmentCategory>();
        // Should only be `false` for weapon slots
        [SerializeField] bool canBeEmpty;
        // Should only be `false` for loop slots
        [SerializeField] bool canPlayerPutItems;

        public Item item { get { return this.currentItem; } }

        public int amount { get { return this.currentAmount; } }

        public Slot(Item initialItem, int initialAmount, List<EquipmentCategory> itemFilter, bool canBeEmpty, bool canPlayerPutItems)
        {
            this.currentItem = initialItem;
            this.currentAmount = initialAmount;
            if (itemFilter == null) throw new System.ArgumentException("itemFilter is null");
            this.itemFilter = itemFilter;
            this.canBeEmpty = canBeEmpty;
            this.canPlayerPutItems = canPlayerPutItems;
            this.Validate();
        }

        public bool IsEmpty()
        {
            return this.currentItem == null && this.currentAmount == 0;
        }

        void SetEmpty()
        {
            this.currentItem = null;
            this.currentAmount = 0;
        }

        public void Validate()
        {
            if (this.itemFilter == null) throw new System.ArgumentException("Slot with item " + this.currentItem + " has null itemFilter");
            if (!this.IsEmpty() && !this.ApplyItemFilter(this.currentItem))
            {
                throw new System.ArgumentException("Item filter " + this.itemFilter[0].name + " doesn't accept initial item " + this.currentItem);
            }
            if ((this.currentItem == null) != (this.currentAmount == 0))
            {
                throw new System.ArgumentException("currentItem must be null if and only if currentAmount is 0");
            }
            if (this.currentAmount > 1 && !this.currentItem.CanStack()) throw new System.ArgumentException("Item " + this.currentItem.name + " can't stack");
        }

        bool ApplyItemFilter(Item candidate)
        {
            if (candidate == null) return this.canBeEmpty;
            else if (this.itemFilter.Count == 0) return true;
            else return candidate is EquippableItem && this.itemFilter.Contains((candidate as EquippableItem).category);
        }

        /*
         * Takes `amount` items from this slot and returns the taken `Item`. This method is designed
         * for shopping actions (where the player has already paid and needs to take the given `amount` of items from the shop).
         *
         * If there are not enough items in this slot, this method will throw an exception.
         */
        public Item TakeAmount(int amount)
        {
            if (this.currentAmount < amount)
            {
                throw new System.ArgumentException("The ItemStack in the slot is too small");
            }

            if (!this.canBeEmpty && this.currentAmount == amount)
            {
                throw new System.ArgumentException("This operation would make this slot empty, which is forbidden");
            }

            Item takenItem = this.currentItem;
            this.currentAmount -= amount;
            if (this.currentAmount == 0) this.currentItem = null;
            return takenItem;
        }

        /*
         * Lets this slot interact with the item on the cursor (in the inventory GUI). This should be called whenever the player clicks
         * on this slot.
         */
        public void InteractWithCursor(Slot cursor)
        {
            if (cursor.IsEmpty())
            {
                if (this.canBeEmpty) {
                    cursor.currentItem = this.currentItem;
                    cursor.currentAmount = this.currentAmount;
                    this.SetEmpty();
                } else {
                    if (this.currentAmount > 1)
                    {
                        cursor.currentItem = this.currentItem;
                        cursor.currentAmount = this.currentAmount - 1;
                        this.currentAmount = 1;
                    }
                    // Else do nothing
                }
            } else {
                if (this.ApplyItemFilter(cursor.currentItem) && this.canPlayerPutItems)
                {
                    if (this.IsEmpty())
                    {
                        this.currentItem = cursor.currentItem;
                        this.currentAmount = cursor.currentAmount;
                        cursor.SetEmpty();
                    }
                    else
                    {
                        if (this.currentItem == cursor.currentItem)
                        {
                            if (this.currentItem.CanStack())
                            {
                                this.currentAmount += cursor.currentAmount;
                                cursor.SetEmpty();
                            }
                        }
                        else
                        {
                            Item newCursorItem = this.currentItem;
                            int newCursorAmount = this.currentAmount;
                            this.currentItem = cursor.currentItem;
                            this.currentAmount = cursor.currentAmount;
                            cursor.currentItem = newCursorItem;
                            cursor.currentAmount = newCursorAmount;
                        }
                    }
                } 
                // Else do nothing
            }
        }
    }
}