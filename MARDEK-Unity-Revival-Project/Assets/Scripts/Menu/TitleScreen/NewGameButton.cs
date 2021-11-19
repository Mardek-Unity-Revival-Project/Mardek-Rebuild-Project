using UnityEngine;
using UnityEngine.UI;

public class NewGameButton : MonoBehaviour
{
    [SerializeField]
    GameObject newGameLabel;

    [SerializeField]
    GameObject newGameFieldObject;
    
    [SerializeField]
    InputField newGameField;
    
    [SerializeField]
    GameObject beginButton;

    public void Awake()
    {
        newGameLabel.SetActive(false);
        newGameFieldObject.SetActive(false);
        beginButton.SetActive(false);
    }

    public void Update()
    {
        beginButton.SetActive(newGameField.text.Length > 0);
    }

    public void ShowNameField()
    {
        newGameLabel.SetActive(true);
        newGameFieldObject.SetActive(true);
    }
}
