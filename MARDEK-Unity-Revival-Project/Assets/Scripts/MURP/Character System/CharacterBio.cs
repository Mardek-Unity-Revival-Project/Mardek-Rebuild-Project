using UnityEngine;

namespace MURP.CharacterSystem
{
    [CreateAssetMenu(menuName ="MURP/CharacterSystem/CharacterBio")]
    public class CharacterBio : ScriptableObject
    {
        [field: SerializeField] public string displayName { get; private set; }
    }
}