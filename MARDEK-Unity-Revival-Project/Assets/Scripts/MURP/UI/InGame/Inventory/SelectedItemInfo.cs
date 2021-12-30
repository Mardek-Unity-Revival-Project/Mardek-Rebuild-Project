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
            this.UpdateInfoText();
        }

        void Update()
        {
            if (this.currentSlot == null || this.currentSlot.IsEmpty())
            {
                if (this.lastItem != null)
                {
                    this.UpdateInfoText();
                }
            }
            else
            {
                if (this.lastItem != this.currentSlot.item || this.lastAmount != this.currentSlot.amount)
                {
                    this.UpdateInfoText();
                }
            }
        }

        void UpdateInfoText()
        {
            foreach (Image panel in new Image[] {this.descriptionPanel, this.skillsPanel, this.propertiesPanel}) panel.color = LOWER_PANEL_BASE_COLOR;
            foreach (Text text in new Text[] {this.descriptionText, this.skillsText, this.propertiesText}) text.color = LOWER_TEXT_BASE_COLOR;
            
            if (this.currentPanelIndex == 0)
            {
                this.descriptionPanel.color = LOWER_PANEL_ACTIVE_COLOR;
                this.descriptionText.color = LOWER_TEXT_ACTIVE_COLOR;
            }
            else if (this.currentPanelIndex == 1)
            {
                this.skillsPanel.color = LOWER_PANEL_ACTIVE_COLOR;
                this.skillsText.color = LOWER_TEXT_ACTIVE_COLOR;
            }
            else
            {
                this.propertiesPanel.color = LOWER_PANEL_ACTIVE_COLOR;
                this.propertiesText.color = LOWER_TEXT_ACTIVE_COLOR;
            }

            if (this.currentSlot == null || this.currentSlot.IsEmpty())
            {
                this.titleText.text = "";
                this.currentInfoText.text = "";
                this.lastItem = null;
                this.lastAmount = 0;
            }
            else
            {
                string titleSuffix = this.currentSlot.amount == 1 ? "" : " x" + this.currentSlot.amount;
                this.titleText.text = this.currentSlot.item.displayName + titleSuffix;

                if (this.currentPanelIndex == 0)
                {
                    this.currentInfoText.text = this.currentSlot.item.description;
                }
                else if (this.currentPanelIndex == 1)
                {
                    this.currentInfoText.text = "List the skills...";
                }
                else
                {
                    this.currentInfoText.text = this.currentSlot.item.properties;
                }

                this.lastItem = this.currentSlot.item;
                this.lastAmount = this.currentSlot.amount;
            }
        }

        public void SetCurrentSlot(Slot newSlot)
        {
            this.currentSlot = newSlot;
            this.UpdateInfoText();
        }

        public void MoveHorizontally(float amount)
        {
            if (amount > 0f)
            {
                this.currentPanelIndex += 1;
                if (this.currentPanelIndex >= 3) this.currentPanelIndex = 0;
            }
            else
            {
                this.currentPanelIndex -= 1;
                if (this.currentPanelIndex < 0) this.currentPanelIndex = 2;
            }
            this.UpdateInfoText();
        }
    }
}