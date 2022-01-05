using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using MURP.Core;
using MURP.Audio;

namespace MURP.UI
{
    public class InGameMenu : SelectableLayout
    {
        [SerializeField] Canvas canvas;
        [SerializeField] AudioObject openMenuSound;
        [SerializeField] AudioObject verticalMenuScrollSound;
        [SerializeField] AudioObject focusMenuSound;
        [SerializeField] AudioObject stopFocusMenuSound;

        private void Awake()
        {
            canvas.enabled = false;
        }

        public void Close()
        {
            if (canvas.enabled == false)
                return;
            canvas.enabled = false;
            PlayerLocks.UISystemLock--;
        }

        public void TryOpen(InputAction.CallbackContext ctx)
        {
            if (canvas.enabled)
                return;
            if (PlayerLocks.EventSystemLock > 0)
                return;

            canvas.enabled = true;
            PlayerLocks.UISystemLock++;
            AudioManager.PlaySoundEffect(openMenuSound);
        }
    }
}