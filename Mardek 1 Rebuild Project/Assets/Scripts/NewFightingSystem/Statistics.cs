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
    public class Statistics : ScriptableObject
    {
        public int STR;
        public int VIT;
        public int SPR;
        public int AGI;
        public int DEF;
        public int MDEF;
        public float EVA; //in %
        public float ACC; //in %
        public int ATK;
        public int HP;
        public int MaxHP;
        public int MP;
        public int MaxMP;
        public int XP;
        public int LVL;
        public bool lock_HP;
        public bool lock_MP;
        public bool IsAlive { get{ return (HP > 0); } }
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
