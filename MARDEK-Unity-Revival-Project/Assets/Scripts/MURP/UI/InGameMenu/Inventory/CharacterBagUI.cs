namespace MURP.UI
{
    public class CharacterBagUI : InventoryCharacterUI
    {
        private void OnEnable()
        {
            AssignInventoryToUI(character.Inventory);
        }

        public void FetchSelectedCharacterAndUpdateUI()
        {
            character = CharacterSelectable.currentSelected.Character;
            AssignInventoryToUI(character.Inventory);
        }
    }
}