using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JRPG
{
    public class MoveDirection : ScriptableObject
    {
        [SerializeField] Vector2 direction = Vector2.zero;
        public Vector2 value
        {
            get { return direction; }
        }

        public void Initialize(Vector2 _direction)
        {
            if (direction == Vector2.zero)
                direction = _direction;
            else
                Debug.LogWarning("You shouldn't change the value of a MoveDirection object");
        }
    }
}
