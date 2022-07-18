using MURP.DiscoverySystem;
using UnityEngine;
using UnityEngine.UI;

namespace MURP.UI
{
    public class MonsterDetails : MonoBehaviour
    {
        [SerializeField] Image elementIcon;
        [SerializeField] Text nameText;
        [SerializeField] Text descriptionText;

        [SerializeField] Text classValue;

        public void SetMonster(EncyclopediaMonster monster)
        {
            elementIcon.sprite = monster.element.thickSprite;
            nameText.text = monster.displayName;
            descriptionText.text = monster.description;
            classValue.text = monster.battleClass;
        }
    }
}
