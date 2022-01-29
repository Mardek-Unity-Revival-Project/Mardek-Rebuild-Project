using MURP.CharacterSystem;
using UnityEngine;

namespace MURP.UI
{
    interface PartyEntry
    {
        void SetCharacter(Character character);

        GameObject gameObject { get; }
    }
}
