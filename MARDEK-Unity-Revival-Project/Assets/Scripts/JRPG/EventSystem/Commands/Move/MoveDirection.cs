using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JRPG
{
    public class MoveDirection : ScriptableObject
    {
        [SerializeField] Vector2 direction;
        public Vector2 value
        {
            get { return direction; }
        }
    }
}
