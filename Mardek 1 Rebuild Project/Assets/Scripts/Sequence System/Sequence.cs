using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Sequence", menuName = "Sequence System/Sequence")]
public class Sequence : ScriptableObject {

    public List<SequenceElement> elementsSequence;
    public void Trigger() {
        foreach (SequenceElement e in elementsSequence) {
            e.Trigger();
        }
    }
}
