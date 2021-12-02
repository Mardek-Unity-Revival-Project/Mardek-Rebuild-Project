using UnityEngine;
using UnityEngine.UI;

public class SubMenuButton : MonoBehaviour
{
    private static readonly Color INACTIVE_COLOR = new Color(245 / 255f, 229 / 255f, 156 / 255f);
    private static readonly Color ACTIVE_COLOR = new Color(110 / 255f, 170 / 255f, 220 / 255f);

    [SerializeField]
    private Text text;

    public void SetActive()
    {
        this.text.color = ACTIVE_COLOR;
    }

    public void SetInactive()
    {
        this.text.color = INACTIVE_COLOR;
    }
}
