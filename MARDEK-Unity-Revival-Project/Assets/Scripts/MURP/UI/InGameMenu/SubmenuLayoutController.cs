using System.Collections.Generic;
using UnityEngine;
using MURP.Audio;

namespace MURP.UI
{
    public class SubmenuLayoutController : SelectableLayout
    {        
        [SerializeField] AudioObject focusSound;
        [SerializeField] AudioObject unfocusSound;
        [SerializeField] GameObject blurPanel;

        public void Unfocus()
        {
            enabled = false;
            blurPanel.SetActive(true);
            AudioManager.PlaySoundEffect(unfocusSound);
        }

        public void Focus()
        {
            enabled = true;
            blurPanel.SetActive(false);
            AudioManager.PlaySoundEffect(focusSound);
        }

        public bool IsFocussed()
        {
            return enabled;
        }
    }
}