using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using MURP.Audio;
using MURP.Core;

namespace MURP.UI
{
    public class InGameMenu : MonoBehaviour
    {
        [SerializeField] AudioObject openMenuSound;
        [SerializeField] UnityEvent OnOpen;
        [SerializeField] UnityEvent OnClose;

        [SerializeField] GameObject sidePanel;
        [SerializeField] GameObject submenus;

        public void Close()
        {
            if (gameObject.activeSelf == false)
                return;
            gameObject.SetActive(false);
            PlayerLocks.UISystemLock--;
            OnClose.Invoke();
        }

        public void TryOpen(InputAction.CallbackContext ctx)
        {
            if (gameObject.activeSelf)
                return;
            if (PlayerLocks.EventSystemLock > 0)
                return;

            gameObject.SetActive(true);
            PlayerLocks.UISystemLock++;
            AudioManager.PlaySoundEffect(openMenuSound);
            OnOpen.Invoke();
        }

        public void ShowEncyclopediaSection(GameObject section)
        {
            sidePanel.SetActive(false);
            submenus.SetActive(false);
            section.SetActive(true);
        }

        public void ExitEncyclopediaSection(GameObject section)
        {
            sidePanel.SetActive(true);
            submenus.SetActive(true);
            section.SetActive(false);
        }
    }
}
