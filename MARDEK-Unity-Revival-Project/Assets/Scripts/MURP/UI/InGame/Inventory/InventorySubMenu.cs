using UnityEngine;
using UnityEngine.UI;
using MURP.CharacterSystem;
using MURP.Inventory;
using System.Collections.Generic;

namespace MURP.UI
{
    public class InventorySubMenu : MonoBehaviour
    {
        const int NUM_EQUIPMENT_SLOTS = 6;
        const int NUM_BASIC_SLOTS = 64;

        [SerializeField] Party playerParty;
        
        [SerializeField] GridLayoutGroup[] slotsLayouts;
        [SerializeField] HorizontalLayoutGroup[] equipmentSlotLayouts;
        [SerializeField] Image[] equipmentBarBackgrounds;
        [SerializeField] GameObject slotPrefab;
        //[SerializeField] SelectedItemInfo selectedItemInfo;
        
        SlotCursor cursorItem;
        //List<SlotGrid> slotGrids;
        int currentCharacterIndex;
        //List<SlotGrid> equipmentSlotGrids;
        Slot cursorSlot = new Slot(null, 0, new List<EquipmentCategory>(), true, true);
        bool isActive = false;
        System.Action focusAction;

        private void Awake()
        {
            //SetupParty();
        }

        private void OnEnable()
        {
            ////foreach (SlotGrid slotGrid in slotGrids)
            //{
            //    //slotGrid.UpdateSlots(cursorSlot, selectedItemInfo, focusAction);
            //}

            //foreach (SlotGrid equipmentGrid in this.equipmentSlotGrids)
            //{
            //    //equipmentGrid.UpdateSlots(this.cursorSlot, this.selectedItemInfo, this.focusAction);
            //}
            //UpdateSelectedInventory();

            ////cursorItem = new CursorSlotUI(cursorSlot);
            ////isActive = true;
        }

        private void OnDisable()
        {
            isActive = false;
        }

        //void Update()
        //{
        //    if (isActive)
        //    {
        //        cursorItem.Update();
        //    }
        //}

        void UpdateSelectedInventory()
        {
            //    foreach (SlotGrid slotGrid in slotGrids)
            //        slotGrid.SetInActive();
            for (int characterIndex = 0; characterIndex < slotsLayouts.Length; characterIndex++)
            {
                slotsLayouts[characterIndex].gameObject.SetActive(characterIndex == currentCharacterIndex);
            }
            UpdateEquipmentBarBackgrounds();
        }

        void UpdateEquipmentBarBackgrounds()
        {
            
            for (int characterIndex = 0; characterIndex < equipmentBarBackgrounds.Length; characterIndex++)
            {
                //var color = characterIndex == currentCharacterIndex ? selectedColor : unselectedColor;
                //equipmentBarBackgrounds[characterIndex].color = color;
            }
        }

        //void SetupParty()
        //{
        //    if (playerParty == null)
        //        throw new System.Exception("Party object not referenced");
        //    currentCharacterIndex = 0;
        //    //ConstructSlots();
        //    for (int characterIndex = 0; characterIndex < equipmentSlotLayouts.Length; characterIndex++)
        //    {
        //        equipmentSlotLayouts[characterIndex].transform.parent.gameObject.SetActive(characterIndex < playerParty.Characters.Count);
        //    }
        //}

        //void ConstructSlots()
        //{
        //    int numCharacters = playerParty.Characters.Count;
        //    slotGrids = new List<SlotGrid>(numCharacters);

        //    for (int characterIndex = 0; characterIndex < numCharacters; characterIndex++)
        //    {
        //        List<GameObject> slotComponents = new List<GameObject>(NUM_BASIC_SLOTS);
        //        for (int index = 0; index < NUM_BASIC_SLOTS; index++)
        //        {
        //            GameObject slotComponent = Instantiate(slotPrefab);
        //            slotComponent.transform.SetParent(slotsLayouts[characterIndex].transform, false);
        //            slotComponents.Add(slotComponent);
        //        }

        //        Inventory.Inventory currentInventory = playerParty.Characters[characterIndex].inventory;
        //        slotGrids.Add(new SlotGrid(slotComponents, currentInventory, NUM_EQUIPMENT_SLOTS, NUM_BASIC_SLOTS));
        //    }

        //    equipmentSlotGrids = new List<SlotGrid>(numCharacters);
        //    for (int characterIndex = 0; characterIndex < numCharacters; characterIndex++)
        //    {
        //        List<GameObject> equipmentSlotComponents = new List<GameObject>(NUM_EQUIPMENT_SLOTS);
        //        for (int slotIndex = 0; slotIndex < NUM_EQUIPMENT_SLOTS; slotIndex++)
        //        {
        //            GameObject slotComponent = Instantiate(slotPrefab);
        //            slotComponent.transform.SetParent(equipmentSlotLayouts[characterIndex].transform, false);
        //            equipmentSlotComponents.Add(slotComponent);
        //        }
        //        SlotGrid equipmentSlotGrid = new SlotGrid( equipmentSlotComponents, playerParty.Characters[characterIndex].inventory, 0, NUM_EQUIPMENT_SLOTS);
        //        equipmentSlotGrids.Add(equipmentSlotGrid);
        //    }
        //}
    }
}
