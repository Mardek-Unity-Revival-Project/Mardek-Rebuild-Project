using UnityEngine;

namespace MURP.Dialogue
{
    [CreateAssetMenu(menuName ="JRPG/CharacterBio")]
    public class CharacterBio : ScriptableObject
    {
        [field: SerializeField] public string displayName { get; private set; }
    }
}