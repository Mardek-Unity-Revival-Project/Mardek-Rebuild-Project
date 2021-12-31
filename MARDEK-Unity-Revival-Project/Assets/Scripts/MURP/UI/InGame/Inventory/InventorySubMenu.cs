using UnityEngine;
using UnityEngine.UI;
using MURP.CharacterSystem;
using MURP.Inventory;
using System.Collections.Generic;

namespace MURP.UI
{
    public class InventorySubMenu : FocusSubMenu
    {
        const int NUM_EQUIPMENT_SLOTS = 6;
        const int NUM_BASIC_SLOTS = 64;

        [SerializeField] GridLayoutGroup[] slotsLayouts;
        [SerializeField] HorizontalLayoutGroup[] equipmentSlotLayouts;
        [SerializeField] Image[] equipmentBarBackgrounds;
        [SerializeField] GameObject slotPrefab;
        [SerializeField] SelectedItemInfo selectedItemInfo;
        
        CursorSlotUI cursorItem;
        Party theParty;
        List<SlotGrid> slotGrids;
        int currentCharacterIndex;
        List<SlotGrid> equipmentSlotGrids;
        Slot cursorSlot = new Slot(null, 0, new List<EquipmentCategory>(), true, true);
        bool isActive = false;
        System.Action focusAction;

        override public void SetActive()
        {
            foreach (SlotGrid slotGrid in this.slotGrids)
            {
                slotGrid.UpdateSlots(this.cursorSlot, this.selectedItemInfo, this.focusAction);
            }
            
            foreach (SlotGrid equipmentGrid in this.equipmentSlotGrids)
            {
                equipmentGrid.UpdateSlots(this.cursorSlot, this.selectedItemInfo, this.focusAction);
            }
            this.UpdateSelectedInventory();

            this.cursorItem = new CursorSlotUI(this.cursorSlot);
            this.isActive = true;
        }

        public override void SetInActive()
        {
            this.isActive = false;
        }

        void Update()
        {
            if (this.isActive)
            {
                this.cursorItem.Update();
            }
        }

        void UpdateSelectedInventory()
        {
            for (int characterIndex = 0; characterIndex < this.slotsLayouts.Length; characterIndex++)
            {
                this.slotsLayouts[characterIndex].gameObject.SetActive(characterIndex == this.currentCharacterIndex);
            }
            this.UpdateEquipmentBarBackgrounds();
        }

        void UpdateEquipmentBarBackgrounds()
        {
            Color unselectedColor = new Color(99f / 255f, 75f / 255f, 44f / 255f);
            Color selectedColor = new Color(41f / 255f, 57f / 255f, 106f / 255f);
            for (int characterIndex = 0; characterIndex < this.equipmentBarBackgrounds.Length; characterIndex++)
            {
                this.equipmentBarBackgrounds[characterIndex].color = characterIndex == this.currentCharacterIndex ? selectedColor : unselectedColor;
            }
        }

        public override bool StopFocus()
        {
            return this.cursorSlot.IsEmpty();
        }

        override public void SetParty(Party theParty)
        {
            this.theParty = theParty;
            this.currentCharacterIndex = 0;
            ConstructSlots();
            for (int characterIndex = 0; characterIndex < this.equipmentSlotLayouts.Length; characterIndex++)
            {
                this.equipmentSlotLayouts[characterIndex].gameObject.transform.parent.gameObject.SetActive(characterIndex < theParty.Characters.Count);
            }
        }

        public override void HandleVerticalMovement(float movement)
        {
            int numCharacters = this.theParty.Characters.Count;

            if (movement > 0f)
            {
                this.currentCharacterIndex--;
                if (this.currentCharacterIndex < 0) this.currentCharacterIndex = numCharacters - 1;
            }
            else
            {
                this.currentCharacterIndex++;
                if (this.currentCharacterIndex >= numCharacters)
                {
                    this.currentCharacterIndex = 0;
                }
            }

            this.UpdateSelectedInventory();
        }

        public override void HandleHorizontalMovement(float amount)
        {
            this.selectedItemInfo.MoveHorizontally(amount);
        }

        public void SetForceFocusAction(System.Action forceFocusAction)
        {
            this.focusAction = forceFocusAction;
        }

        void ConstructSlots()
        {
            int numCharacters = this.theParty.Characters.Count;
            this.slotGrids = new List<SlotGrid>(numCharacters);

            for (int characterIndex = 0; characterIndex < numCharacters; characterIndex++)
            {
                List<GameObject> slotComponents = new List<GameObject>(NUM_BASIC_SLOTS);
                for (int index = 0; index < NUM_BASIC_SLOTS; index++)
                {
                    GameObject slotComponent = Instantiate(this.slotPrefab);
                    slotComponent.transform.SetParent(this.slotsLayouts[characterIndex].transform, false);
                    slotComponents.Add(slotComponent);
                }

                Inventory.Inventory currentInventory = this.theParty.Characters[characterIndex].inventory;
                this.slotGrids.Add(new SlotGrid(slotComponents, currentInventory, NUM_EQUIPMENT_SLOTS, NUM_BASIC_SLOTS));
            }

            this.equipmentSlotGrids = new List<SlotGrid>(numCharacters);
            for (int characterIndex = 0; characterIndex < numCharacters; characterIndex++)
            {
                List<GameObject> equipmentSlotComponents = new List<GameObject>(NUM_EQUIPMENT_SLOTS);
                for (int slotIndex = 0; slotIndex < NUM_EQUIPMENT_SLOTS; slotIndex++)
                {
                    GameObject slotComponent = Instantiate(this.slotPrefab);
                    slotComponent.transform.SetParent(this.equipmentSlotLayouts[characterIndex].transform, false);
                    equipmentSlotComponents.Add(slotComponent);
                }

                SlotGrid equipmentSlotGrid = new SlotGrid(equipmentSlotComponents, this.theParty.Characters[characterIndex].inventory, 0, NUM_EQUIPMENT_SLOTS);
                this.equipmentSlotGrids.Add(equipmentSlotGrid);
            }
        }
    }
}
