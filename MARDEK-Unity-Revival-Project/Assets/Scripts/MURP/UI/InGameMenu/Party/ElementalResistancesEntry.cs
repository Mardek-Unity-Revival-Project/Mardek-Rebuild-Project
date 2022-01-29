using MURP.CharacterSystem;
using System.Collections.Generic;
using UnityEngine;

namespace MURP.UI
{
    public class ElementalResistancesEntry : MonoBehaviour, PartyEntry
    {
        [SerializeField] List<SingleElementResistance> elements;

        public void SetCharacter(Character character)
        {
            foreach (var element in this.elements)
            {
                element.SetCharacter(character);
            }
        }
    }
}
