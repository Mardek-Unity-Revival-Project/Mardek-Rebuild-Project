using UnityEngine;

namespace MURP.CharacterSystem
{
    [CreateAssetMenu(menuName ="JRPG/CharacterBio")]
    public class CharacterBio : ScriptableObject
    {
        [field: SerializeField] public string displayName { get; private set; }
    }
}