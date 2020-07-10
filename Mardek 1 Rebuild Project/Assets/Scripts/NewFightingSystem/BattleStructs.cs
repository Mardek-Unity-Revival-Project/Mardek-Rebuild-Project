using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.NewFightingSystem
{
        /// <summary>
        /// The Attack struct defines a single attack and its effects
        /// </summary>
        public struct Attack
        {
            /// <summary>
            /// The power of the attack
            /// </summary>
            public int power;
            /// <summary>
            /// How much (magic) defense this attack ignores
            /// </summary>
            public int pierce;
            /// <summary>
            /// Is this considered a magic attack?
            /// </summary>
            public bool isMagic;
            /// <summary>
            /// The Element of the attack
            /// </summary>
            public Element element;
            /// <summary>
            /// Does the attack inflict MP damage?
            /// </summary>
            public bool isMP;
            public Dictionary<StatusEffect, float> effects;

            public Attack(int power, int pierce, bool isMagic, Element element, bool isMP, Dictionary<StatusEffect, float> effects)
            {
                this.power = power;
                this.pierce = pierce;
                this.isMagic = isMagic;
                this.element = element;
                this.isMP = isMP;
                this.effects = effects;
            }
        }
}
