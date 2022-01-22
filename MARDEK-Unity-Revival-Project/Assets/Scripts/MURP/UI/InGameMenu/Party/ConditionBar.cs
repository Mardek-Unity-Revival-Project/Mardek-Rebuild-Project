using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MURP.UI
{
    public class ConditionBar : MonoBehaviour
    {
        [SerializeField] List<Color> textColors;
        [SerializeField] List<Color> backgroundColors;

        [SerializeField] Image background;
        [SerializeField] Text currentValue;
        [SerializeField] Text maxValue;

        public void SetValues(int currentValue, int maxValue)
        {
            if (this.backgroundColors.Count != this.textColors.Count) throw new System.ArgumentException("Must have same number of text colors as background colors");
            if (this.backgroundColors.Count < 1) throw new System.ArgumentException("Must have at least 1 background color");

            float currentFraction = (float) currentValue / (float) maxValue;
            int colorIndex = (int) (currentFraction * (this.backgroundColors.Count - 1) + 0.5f);

            this.background.transform.localScale = new Vector2(currentFraction, 1f);
            this.background.color = this.backgroundColors[colorIndex];
            this.currentValue.color = this.textColors[colorIndex];
            this.maxValue.color = this.textColors[colorIndex];
        }
    }
}
