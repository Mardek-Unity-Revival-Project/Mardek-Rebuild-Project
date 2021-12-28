using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MURP.StatsSystem;

namespace MURP.UI
{
    public class UIStatBar : MonoBehaviour
    {
        [SerializeField] CharacterUI characterUI = null;
        [SerializeField] IntegerStat stat;
        [SerializeField] IntegerStat maxStat;
        [SerializeField] RectTransform barTransform;
        [SerializeField] TMPro.TMP_Text statText;
        [SerializeField] TMPro.TMP_Text maxStatText;

        private void Update()
        {
            // TODO: only update when the character stats change
            UpdateBar();
        }

        [ContextMenu("Update Bar")]
        void UpdateBar()
        {
            var statValue = (float)characterUI.character.GetStat(stat).Value;
            if (statText) statText.text = statValue.ToString();
            var maxStatValue = (float)characterUI.character.GetStat(maxStat).Value;
            if (maxStatText) maxStatText.text = maxStatValue.ToString();
            if (barTransform)
            {
                float xScale = statValue / maxStatValue;
                barTransform.localScale = new Vector3(xScale, 1, 1);
            }
        }
    }
}