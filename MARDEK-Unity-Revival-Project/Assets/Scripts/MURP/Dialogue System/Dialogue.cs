using System.Collections.Generic;
using UnityEngine;

namespace MURP.DialogueSystem
{
    [CreateAssetMenu(menuName = "MURP/DialogueSystem/Dialogue")]
    public class Dialogue : ScriptableObject
    {
        [field:SerializeField] public List<CharacterLines> CharacterLines { get; private set; }
    }
}