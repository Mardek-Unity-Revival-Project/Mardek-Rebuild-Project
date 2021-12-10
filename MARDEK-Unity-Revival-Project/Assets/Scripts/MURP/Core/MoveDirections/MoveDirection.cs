using System;
using UnityEngine;

namespace MURP.Core
{
    public class MoveDirection : AddressableScriptableObject
    {
        [SerializeField] Vector2 direction = Vector2.zero;
        public Vector2 value
        {
            get { return direction; }
        }
    }
}