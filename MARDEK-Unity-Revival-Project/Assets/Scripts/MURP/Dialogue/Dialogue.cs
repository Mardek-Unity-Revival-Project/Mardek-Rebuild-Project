using System.Collections.Generic;
using UnityEngine;

namespace MURP.Dialogue
{
    [CreateAssetMenu(menuName = "JRPG/Dialogue")]
    public class Dialogue : ScriptableObject
    {
        [field:SerializeField] public List<CharacterLines> CharacterLines { get; private set; }
    }
}