using MURP.DiscoverySystem;
using UnityEngine;
using UnityEngine.UI;

namespace MURP.UI
{
    public class PersonDetails : MonoBehaviour
    {
        [SerializeField] Image elementIcon;
        [SerializeField] Text fullNameText;
        [SerializeField] Text descriptionText;

        [SerializeField] Image portrait;
        [SerializeField] Text raceValue;
        [SerializeField] Text genderValue;
        [SerializeField] Text ageValue;
        [SerializeField] Text classValue;
        [SerializeField] Text elementValue;
        [SerializeField] Text placeOfOriginValue;
        [SerializeField] Text weaponValue;
        [SerializeField] Text alignmentValue;

        public void SetPerson(EncyclopediaPerson person)
        {
            elementIcon.sprite = person.element.thickSprite;
            fullNameText.text = person.fullName;
            descriptionText.text = person.fullDescription;

            portrait.sprite = person.portrait;
            raceValue.text = person.race;
            genderValue.text = person.gender;
            ageValue.text = person.age.ToString();
            classValue.text = person.battleClass;
            elementValue.text = person.element.name.ToUpper();
            placeOfOriginValue.text = person.placeOfOrigin;
            weaponValue.text = person.weapon;
            alignmentValue.text = person.alignment;
        }
    }
}
