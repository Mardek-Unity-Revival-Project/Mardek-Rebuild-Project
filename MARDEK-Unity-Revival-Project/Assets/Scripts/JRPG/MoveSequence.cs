using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JRPG {
    [CreateAssetMenu(menuName = "JRPG/MoveSequence")]
    public class MoveSequence : Sequence
    {
        [SerializeField] Movement target = null;
        [SerializeField] List<Vector2> moveDirections = new List<Vector2>();

        public override void Trigger()
        {
            base.Trigger();
            target = FindObjectOfType<Movement>();
            foreach(Vector2 move in moveDirections)
            {
                target.Move(move);
            }
        }
    } 
}
