using UnityEngine;
using UnityEngine.UI;

public class SubMenuButton : MonoBehaviour
{
    private static readonly Color INACTIVE_COLOR = new Color(245 / 255f, 229 / 255f, 156 / 255f);
    private static readonly Color ACTIVE_COLOR = new Color(110 / 255f, 170 / 255f, 220 / 255f);
    private static readonly Color FADED_INACTIVE_COLOR = new Color(210 / 255f, 190 / 255f, 180 / 255f);
    private static readonly Color FADED_ACTIVE_COLOR = new Color(150 / 255f, 170 / 255f, 200 / 255f);

    [SerializeField]
    private Text text;

    [SerializeField]
    private bool isDeep;

    public bool IsDeep()
    {
        return isDeep;
    }

    public void SetActive()
    {
        this.text.color = ACTIVE_COLOR;
        // TODO Show the actual sub menu of this button
    }

    public void SetInactive()
    {
        this.text.color = INACTIVE_COLOR;
    }

    public void StartFade()
    {
        if (this.text.color == ACTIVE_COLOR) {
            this.text.color = FADED_ACTIVE_COLOR;
        } else {
            this.text.color = FADED_INACTIVE_COLOR;
        }
    }

    public void StopFade()
    {
        if (this.text.color == FADED_ACTIVE_COLOR) {
            this.text.color = ACTIVE_COLOR;
        } else {
            this.text.color = INACTIVE_COLOR;
        }
    }

    public void Focus()
    {
        // TODO Focus on the sub menu of this button
    }

    public void StopFocus()
    {
        // TODO Stop focussing on the sub menu of this button
    }
}
