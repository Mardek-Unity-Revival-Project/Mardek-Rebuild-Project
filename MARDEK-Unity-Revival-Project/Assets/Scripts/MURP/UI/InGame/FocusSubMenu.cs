namespace MURP.UI
{
    public class FocusSubMenu : SubMenu
    {
        public virtual void StartFocus() {}

        // Returns false if the submenu refuses to stop focussing (for instance the inventory submenu when the cursor has an item)
        public virtual bool StopFocus()
        {
            return true;
        }
    }
}