using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Dialogue", menuName = "Sequence System/Dialogue")]
public class Dialogue : SequenceElement {

    [SerializeField]
    private List<Speech> speeches = null;

    public override void Trigger() {
        foreach(Speech speech in speeches) {
            speech.Show();
        }
    }
}

[System.Serializable]
public class Speech {
    [SerializeField] private Character speaker = null;
    [SerializeField][TextArea] private string text = null;

    public void Show() {
        Debug.Log(speaker.displayName+"(" + speaker.element+"): "+ text);
    }
}
