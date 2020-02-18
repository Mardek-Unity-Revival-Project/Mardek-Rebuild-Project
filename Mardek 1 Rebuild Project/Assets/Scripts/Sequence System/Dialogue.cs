using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Dialogue", menuName = "Sequence System/Dialogue")]
public class Dialogue : SequenceElement
{
    public string speaker;
    public string line;

    public override void Trigger() {
        Debug.Log(speaker + ": " + line);
    }

}
