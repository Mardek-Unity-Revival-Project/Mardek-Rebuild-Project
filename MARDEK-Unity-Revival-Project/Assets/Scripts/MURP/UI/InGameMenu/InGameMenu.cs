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
    }
}
