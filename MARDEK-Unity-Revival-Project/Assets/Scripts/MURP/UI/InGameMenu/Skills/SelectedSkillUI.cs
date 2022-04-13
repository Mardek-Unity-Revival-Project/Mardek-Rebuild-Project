using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MURP.UI
{
    public class SelectedSkillUI : MonoBehaviour
    {
        [SerializeField] Text selectedSkillName;
        [SerializeField] Text selectedSkillDescription;
        [SerializeField] Text bottomBarDescription;
        [SerializeField] Image selectedSkillElement;
        [SerializeField] ConditionBar mpBar;
        [SerializeField] ConditionBar rpBar;

        private void ShowMpBar()
        {
            this.mpBar.gameObject.SetActive(true);
            this.rpBar.gameObject.SetActive(false);
            this.bottomBarDescription.text = "MP";
            this.mpBar.SetValues(100, 150); // TODO Improve this once we actually have mana
        }
    }
}