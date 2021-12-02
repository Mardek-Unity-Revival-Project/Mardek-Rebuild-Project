using UnityEngine;
using System.Collections;

namespace JRPG {
    [CreateAssetMenu(menuName ="JRPG/CharacterBio")]
    public class CharacterBio : ScriptableObject
    {
        [field: SerializeField] public string displayName { get; private set; }
    }
}