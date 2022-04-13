using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MURP.UI
{
    public class SelectableWithCurrentSelected<T> : Selectable where T : SelectableWithCurrentSelected<T>
    {
        public static T currentSelected { get; private set; }

        public override void Select(bool playSFX = true)
        {
            currentSelected = this as T;
            base.Select(playSFX);
        }
    }
}