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

        [SerializeField] Party playerParty;
        
        [SerializeField] GridLayoutGroup[] slotsLayouts;
        [SerializeField] HorizontalLayoutGroup[] equipmentSlotLayouts;
        [SerializeField] Image[] equipmentBarBackgrounds;
        [SerializeField] GameObject slotPrefab;
        [SerializeField] SelectedItemInfo selectedItemInfo;
        
        CursorSlotUI cursorItem;
        List<SlotGrid> slotGrids;
        int currentCharacterIndex;
        List<SlotGrid> equipmentSlotGrids;
        Slot cursorSlot = new Slot(null, 0, new List<EquipmentCategory>(), true, true);
        bool isActive = false;
        System.Action focusAction;

        private void Awake()
        {
            SetupParty();
        }

        override public void SetActive()
        {
            foreach (SlotGrid slotGrid in slotGrids)
            {
                slotGrid.UpdateSlots(cursorSlot, selectedItemInfo, focusAction);
            }
            
            foreach (SlotGrid equipmentGrid in this.equipmentSlotGrids)
            {
                equipmentGrid.UpdateSlots(this.cursorSlot, this.selectedItemInfo, this.focusAction);
            }
            UpdateSelectedInventory();

            cursorItem = new CursorSlotUI(cursorSlot);
            isActive = true;
        }

        public override void SetInActive()
        {
            isActive = false;
        }

        void Update()
        {
            if (isActive)
            {
                cursorItem.Update();
            }
        }

        void UpdateSelectedInventory()
        {
            foreach (SlotGrid slotGrid in slotGrids)
                slotGrid.SetInActive();
            for (int characterIndex = 0; characterIndex < slotsLayouts.Length; characterIndex++)
            {
                slotsLayouts[characterIndex].gameObject.SetActive(characterIndex == currentCharacterIndex);
            }
            UpdateEquipmentBarBackgrounds();
        }

        void UpdateEquipmentBarBackgrounds()
        {
            Color unselectedColor = new Color(99f / 255f, 75f / 255f, 44f / 255f);
            Color selectedColor = new Color(41f / 255f, 57f / 255f, 106f / 255f);
            for (int characterIndex = 0; characterIndex < equipmentBarBackgrounds.Length; characterIndex++)
            {
                var color = characterIndex == currentCharacterIndex ? selectedColor : unselectedColor;
                equipmentBarBackgrounds[characterIndex].color = color;
            }
        }

        public override bool StopFocus()
        {
            return cursorSlot.IsEmpty();
        }

        void SetupParty()
        {
            if (playerParty == null)
                throw new System.Exception("Party object not referenced");
            currentCharacterIndex = 0;
            ConstructSlots();
            for (int characterIndex = 0; characterIndex < equipmentSlotLayouts.Length; characterIndex++)
            {
                equipmentSlotLayouts[characterIndex].transform.parent.gameObject.SetActive(characterIndex < playerParty.Characters.Count);
            }
        }

        public override void HandleVerticalMovement(float movement)
        {
            int numCharacters = playerParty.Characters.Count;
            if (movement > 0f)
            {
                currentCharacterIndex--;
                if (currentCharacterIndex < 0) 
                    currentCharacterIndex = numCharacters - 1;
            }
            else if (movement < 0f)
            {
                currentCharacterIndex++;
                if (currentCharacterIndex >= numCharacters)
                    currentCharacterIndex = 0;
            }
            UpdateSelectedInventory();
        }

        public override void HandleHorizontalMovement(float amount)
        {
            selectedItemInfo.MoveHorizontally(amount);
        }

        public void SetForceFocusAction(System.Action forceFocusAction)
        {
            focusAction = forceFocusAction;
        }

        void ConstructSlots()
        {
            int numCharacters = playerParty.Characters.Count;
            slotGrids = new List<SlotGrid>(numCharacters);

            for (int characterIndex = 0; characterIndex < numCharacters; characterIndex++)
            {
                List<GameObject> slotComponents = new List<GameObject>(NUM_BASIC_SLOTS);
                for (int index = 0; index < NUM_BASIC_SLOTS; index++)
                {
                    GameObject slotComponent = Instantiate(slotPrefab);
                    slotComponent.transform.SetParent(slotsLayouts[characterIndex].transform, false);
                    slotComponents.Add(slotComponent);
                }

                Inventory.Inventory currentInventory = playerParty.Characters[characterIndex].inventory;
                slotGrids.Add(new SlotGrid(slotComponents, currentInventory, NUM_EQUIPMENT_SLOTS, NUM_BASIC_SLOTS));
            }

            equipmentSlotGrids = new List<SlotGrid>(numCharacters);
            for (int characterIndex = 0; characterIndex < numCharacters; characterIndex++)
            {
                List<GameObject> equipmentSlotComponents = new List<GameObject>(NUM_EQUIPMENT_SLOTS);
                for (int slotIndex = 0; slotIndex < NUM_EQUIPMENT_SLOTS; slotIndex++)
                {
                    GameObject slotComponent = Instantiate(slotPrefab);
                    slotComponent.transform.SetParent(equipmentSlotLayouts[characterIndex].transform, false);
                    equipmentSlotComponents.Add(slotComponent);
                }
                SlotGrid equipmentSlotGrid = new SlotGrid( equipmentSlotComponents, playerParty.Characters[characterIndex].inventory, 0, NUM_EQUIPMENT_SLOTS);
                equipmentSlotGrids.Add(equipmentSlotGrid);
            }
        }
    }
}
