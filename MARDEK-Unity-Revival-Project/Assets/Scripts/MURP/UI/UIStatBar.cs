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

        private void Update()
        {
            // TODO: only update when the character stats change
            UpdateBar();
        }

        [ContextMenu("Update Bar")]
        void UpdateBar()
        {
            float xScale = (float) characterUI.character.GetStat(stat).Value / characterUI.character.GetStat(maxStat).Value;
            GetComponent<RectTransform>().localScale = new Vector3(xScale, 1, 1);
        }
    }
}