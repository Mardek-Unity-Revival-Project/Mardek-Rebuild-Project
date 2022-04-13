using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MURP.UI
{
    public class TintTextOrImage: MonoBehaviour
    {
        [SerializeField] Text text;
        [SerializeField] Color textColor;
        [Space]
        [SerializeField] Image image;
        [SerializeField] Color imageColor;

        public void ApplyTint()
        {
            if (text)
                text.color = textColor;
            if (image)
                image.color = imageColor;
        }
    }
}
