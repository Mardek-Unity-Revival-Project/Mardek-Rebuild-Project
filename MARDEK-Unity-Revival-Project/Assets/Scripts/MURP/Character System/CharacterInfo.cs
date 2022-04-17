using UnityEngine;
using MURP.Core;
using MURP.Animation;
using MURP.SkillSystem;

namespace MURP.CharacterSystem
{
    [CreateAssetMenu(menuName ="MURP/CharacterSystem/CharacterInfo")]
    public class CharacterInfo : AddressableScriptableObject
    {
        [field: SerializeField] public string displayName { get; private set; }
        [field: SerializeField] public SpriteAnimationClipList WalkSprites { get; private set; }
        [field: SerializeField] public Skillset ActionSkillset { get; private set; }
    }
}