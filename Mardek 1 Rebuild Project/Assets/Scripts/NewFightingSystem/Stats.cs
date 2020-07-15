using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.NewFightingSystem
{
    /// <summary>
    /// The Statistics class holds all the important entity specific statistics 
    /// </summary>
    public class Stats : ScriptableObject
    {
        public int STR { get; set; }
        public int VIT { get; set; }
        public int SPR { get; set; }
        public int AGI { get; set; }
        public int DEF { get; set; }
        public int MDEF { get; set; }
        public float EVA { get; set; }
        public float ACC { get; set; }
        public int ATK { get; set; }
        public int HP { get; set; }
        public int MaxHP { get { return (3 * VIT + 2 * VIT * LVL); } }
        public int MP { get; set; }
        public int MaxMP { get { return (SPR * 17 / 6 + SPR * LVL / 6); } }
        public int XP { get; set; }
        public int LVL { get; set; }
        public bool Lock_HP { get; set; }
        public bool Lock_MP { get; set; }

        public bool IsAlive { get { return (HP > 0); } }

        public StatusEffect currentEffects;
        public Dictionary<StatusEffect, float> StatusResists;
        public Dictionary<Element, float> ElementResists;
        public Dictionary<Element, bool> IsNullingElement = new Dictionary<Element, bool> {
            {Element.Light, false },
            {Element.Dark, false },
            {Element.Air, false },
            {Element.Earth, false },
            {Element.Water, false },
            {Element.Fire, false },
            {Element.Aether, false },
            {Element.Fig, false }
        };
    }
}
