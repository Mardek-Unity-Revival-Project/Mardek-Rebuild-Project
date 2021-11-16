using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NewGameButton : MonoBehaviour, IPointerClickHandler
{
    public GameObject newGameLabel;
    public GameObject newGameField;
    public GameObject beginButton;

    public void Awake()
    {
        newGameLabel.SetActive(false);
        newGameField.SetActive(false);
        beginButton.SetActive(false);
    }

    public void Update()
    {
        beginButton.SetActive(newGameField.GetComponent<InputField>().text.Length > 0);
    }

    public void OnPointerClick(PointerEventData clickEvent)
    {
        newGameLabel.SetActive(true);
        newGameField.SetActive(true);
    }
}
