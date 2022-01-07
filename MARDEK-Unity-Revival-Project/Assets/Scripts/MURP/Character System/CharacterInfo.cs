using UnityEngine;
using MURP.Core;

namespace MURP.CharacterSystem
{
    [CreateAssetMenu(menuName ="MURP/CharacterSystem/CharacterInfo")]
    public class CharacterInfo : AddressableScriptableObject
    {
        [field: SerializeField] public string displayName { get; private set; }
    }
}