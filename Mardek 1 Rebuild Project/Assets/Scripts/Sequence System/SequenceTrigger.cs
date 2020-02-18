using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// test trigger to the sequence system
public class SequenceTrigger : MonoBehaviour
{
    public Sequence sequence = null;
    // Start is called before the first frame update
    void Start()
    {
        sequence.Trigger();
    }
}
