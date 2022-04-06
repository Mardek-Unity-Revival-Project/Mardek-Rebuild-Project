using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace MURP.SkillSystem
{
    [CreateAssetMenu(menuName = "MURP/SkillSystem/Skillset")]
    public class Skillset : ScriptableObject
    {
        [field: SerializeField] public string Description { get; private set; }
        [field: SerializeField] public Sprite Sprite { get; private set; }
        [field: SerializeField] public List<Skill> Skills { get; private set; }
    }
}