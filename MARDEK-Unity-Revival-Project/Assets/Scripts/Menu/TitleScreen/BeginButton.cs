using JRPG;
using UnityEngine;
using UnityEngine.UI;

public class BeginButton : MonoBehaviour
{

    [SerializeField]
    InputField nameInputField;

    [SerializeField]
    TransitionCommand startGameTransition;

    public void BeginGame()
    {
        Debug.Log("Begin " + nameInputField.text + "...");
        // TODO The save name (nameInputField.text) will need to be remembered eventually
        startGameTransition.Trigger();
    }
}
