using UnityEngine;
using MURP.Core;
using MURP.Animation;

namespace MURP.CharacterSystem
{
    [CreateAssetMenu(menuName ="MURP/CharacterSystem/CharacterInfo")]
    public class CharacterInfo : AddressableScriptableObject
    {
        [field: SerializeField] public string displayName { get; private set; }
        [field: SerializeField] public SpriteAnimationClipList WalkSprites { get; private set; }
    }
}