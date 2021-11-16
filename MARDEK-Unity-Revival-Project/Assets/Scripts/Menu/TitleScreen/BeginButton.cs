using JRPG;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BeginButton : MonoBehaviour, IPointerClickHandler
{

    public InputField nameInputField;

    public TransitionCommand startGameTransition;

    public void OnPointerClick(PointerEventData clickEvent)
    {
        Debug.Log("Begin " + nameInputField.text + "...");
        startGameTransition.Trigger();
    }
}
