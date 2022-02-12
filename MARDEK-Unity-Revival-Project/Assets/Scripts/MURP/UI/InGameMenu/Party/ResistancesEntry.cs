using MURP.CharacterSystem;
using System.Collections.Generic;
using UnityEngine;

namespace MURP.UI
{
    public class ResistancesEntry : MonoBehaviour, PartyEntry
    {
        [SerializeField] List<SingleResistance> elements;

        public void SetCharacter(Character character)
        {
            foreach (var element in this.elements)
            {
                element.SetCharacter(character);
            }
        }
    }
}
