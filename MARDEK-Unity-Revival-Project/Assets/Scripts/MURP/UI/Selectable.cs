using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MURP.UI
{
    public abstract class Selectable: MonoBehaviour
    {
        public abstract void Select();
        public abstract void Deselect();
    }
}