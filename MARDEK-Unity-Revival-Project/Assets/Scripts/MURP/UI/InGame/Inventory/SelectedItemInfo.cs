using UnityEngine;
using UnityEngine.UI;
using MURP.Inventory;

namespace MURP.UI
{
    public class SelectedItemInfo : MonoBehaviour
    {
        static readonly Color LOWER_PANEL_BASE_COLOR = new Color(56f / 255f, 40f / 255f, 23f / 255f, 100f / 255f);
        static readonly Color LOWER_PANEL_ACTIVE_COLOR = new Color(84f / 255f, 64f / 255f, 39f / 255f, 170f / 255f);
        static readonly Color LOWER_TEXT_BASE_COLOR = new Color(104f / 255f, 95f / 255f, 88f / 255f);
        static readonly Color LOWER_TEXT_ACTIVE_COLOR = new Color(238f / 255f, 203f / 255f, 127f / 255f);

        [SerializeField] Text titleText;
        [SerializeField] Text currentInfoText;
        [SerializeField] Image descriptionPanel, skillsPanel, propertiesPanel;
        [SerializeField] Text descriptionText, skillsText, propertiesText;

        const int NUM_PANELS = 3;

        int currentPanelIndex = 0;
        Slot currentSlot = null;
        Item lastItem = null;
        int lastAmount = 0;

        void Start()
        {
            UpdateInfoText();
        }

        void Update()
        {
            if (currentSlot == null || currentSlot.IsEmpty())
            {
                if (lastItem != null)
                {
                    UpdateInfoText();
                }
            }
            else
            {
                if (lastItem != currentSlot.item || lastAmount != currentSlot.amount)
                {
                    UpdateInfoText();
                }
            }
        }

        void UpdateInfoText()
        {
            foreach (Image panel in new Image[] {descriptionPanel, skillsPanel, propertiesPanel}) panel.color = LOWER_PANEL_BASE_COLOR;
            foreach (Text text in new Text[] {descriptionText, skillsText, propertiesText}) text.color = LOWER_TEXT_BASE_COLOR;
            
            if (currentPanelIndex == 0)
            {
                descriptionPanel.color = LOWER_PANEL_ACTIVE_COLOR;
                descriptionText.color = LOWER_TEXT_ACTIVE_COLOR;
            }
            else if (currentPanelIndex == 1)
            {
                skillsPanel.color = LOWER_PANEL_ACTIVE_COLOR;
                skillsText.color = LOWER_TEXT_ACTIVE_COLOR;
            }
            else
            {
                propertiesPanel.color = LOWER_PANEL_ACTIVE_COLOR;
                propertiesText.color = LOWER_TEXT_ACTIVE_COLOR;
            }

            if (currentSlot == null || currentSlot.IsEmpty())
            {
                titleText.text = "";
                currentInfoText.text = "";
                lastItem = null;
                lastAmount = 0;
            }
            else
            {
                string titleSuffix = currentSlot.amount == 1 ? "" : " x" + currentSlot.amount;
                titleText.text = currentSlot.item.displayName + titleSuffix;

                if (currentPanelIndex == 0)
                {
                    currentInfoText.text = currentSlot.item.description;
                }
                else if (currentPanelIndex == 1)
                {
                    currentInfoText.text = "List the skills...";
                }
                else
                {
                    currentInfoText.text = currentSlot.item.properties;
                }

                lastItem = currentSlot.item;
                lastAmount = currentSlot.amount;
            }
        }

        public void SetCurrentSlot(Slot newSlot)
        {
            currentSlot = newSlot;
            UpdateInfoText();
        }

        public void MoveHorizontally(float amount)
        {
            if (amount > 0f)
            {
                currentPanelIndex += 1;
                if (currentPanelIndex >= 3) currentPanelIndex = 0;
            }
            else
            {
                currentPanelIndex -= 1;
                if (currentPanelIndex < 0) currentPanelIndex = 2;
            }
            UpdateInfoText();
        }
    }
}