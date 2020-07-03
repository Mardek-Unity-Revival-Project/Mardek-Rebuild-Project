using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.NewFightingSystem
{
        public struct Attack
        {
            public int power;
            public int pierce;
            public bool isMagic;
            public Element element;
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
