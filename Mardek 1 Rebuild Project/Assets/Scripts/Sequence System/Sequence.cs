using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Sequence", menuName = "Sequence System/Sequence")]
public class Sequence : SequenceElement {

    public List<SequenceElement> elementsSequence;
    public override void Trigger() {
        foreach (SequenceElement e in elementsSequence) {
            if (e != null) {
                e.Trigger();
            }
            else {
                Debug.LogWarning("The sequence contains a null element");
            }
        }
    }
}
