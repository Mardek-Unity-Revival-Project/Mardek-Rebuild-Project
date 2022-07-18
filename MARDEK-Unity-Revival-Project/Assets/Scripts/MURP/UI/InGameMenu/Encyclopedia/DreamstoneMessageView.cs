using MURP.DiscoverySystem;
using UnityEngine;
using UnityEngine.UI;

namespace MURP.UI
{
    public class DreamstoneMessageView : MonoBehaviour
    {
        [SerializeField] Text text;
        [SerializeField] GameObject previousMenu;

        Dreamstone currentDreamstone;
        int currentMessageIndex;

        public void OpenDreamstone(Dreamstone dreamstone)
        {
            currentDreamstone = dreamstone;
            currentMessageIndex = 0;
            this.gameObject.SetActive(true);
            previousMenu.SetActive(false);

            UpdateMessage();
        }

        void UpdateMessage()
        {
            this.text.text = currentDreamstone.messages[currentMessageIndex].text;
            this.text.color = currentDreamstone.messages[currentMessageIndex].element.textColor;
        }

        public void ShowNextMessage()
        {
            if (currentMessageIndex + 1 < currentDreamstone.messages.Count)
            {
                currentMessageIndex += 1;
                UpdateMessage();
            }
            else
            {
                currentDreamstone.isNew = false;
                GoBack();
            }
        }

        public void GoBack()
        {
            this.gameObject.SetActive(false);
            previousMenu.SetActive(true);
        }
    }
}
