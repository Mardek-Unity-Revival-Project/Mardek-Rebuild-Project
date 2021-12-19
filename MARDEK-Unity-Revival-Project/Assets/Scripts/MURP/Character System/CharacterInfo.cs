using UnityEngine;

namespace MURP.CharacterSystem
{
    [CreateAssetMenu(menuName ="MURP/CharacterSystem/CharacterInfo")]
    public class CharacterInfo : ScriptableObject
    {
        [field: SerializeField] public string displayName { get; private set; }
    }
}