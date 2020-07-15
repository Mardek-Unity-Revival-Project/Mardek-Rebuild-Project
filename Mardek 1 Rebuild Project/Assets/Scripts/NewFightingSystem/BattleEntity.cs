using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.NewFightingSystem
{
    abstract class BattleEntity : MonoBehaviour
    {
        public Stats stats;

        /// <summary>
        /// Deal damage to a target, optionally applying one or more status effects.
        /// </summary>
        /// <param name="target">The target of the attack.</param>
        /// <param name="power">The base damage of the attack.</param>
        /// <param name="pierce">The amount of defense to ignore</param>
        /// <param name="isMagic">Is the attack magical in nature (instead of physical)?</param>
        /// <param name="element">The element of the attack.</param>
        /// <param name="isMP">Optional. Does the attack deal MP damage?</param>
        /// <param name="effects">Optional. Applies various status effects. Negative chances = chance to remove the status effect</param>
        /// <returns>The amount of damage dealt</returns>
        /// <remarks>DealDamage is called once for each hit of an attack.
        /// DealDamage is only concerned with the attack side of the equation. TakeDamage and InflictStatus handles the aftermath.
        /// </remarks>
        public virtual int DealDamage(BattleEntity target, int power, int pierce, bool isMagic, Element element, bool isMP = false, Dictionary<StatusEffect, float> effects = null)
        {
            double damage = 0;
            //TODO:calculate damage, taking into account the bonuses from equipment/skills/etc

            //attempt to inflict added status effects, based on their given chance
            if (!(effects is null))
            {
                foreach (StatusEffect effect in effects.Keys)
                {
                    target.ModifyStatus(effect, effects[effect]);
                } 
            }
            return target.TakeDamage((int)damage, element, isMP);
        }

        /// <summary>
        /// Deal damage using an attack
        /// </summary>
        /// <param name="target">The target to deal damage to</param>
        /// <param name="attack">The attack to use</param>
        /// <returns>The amount of damage dealt</returns>
        public virtual int DealDamage(BattleEntity target, Attack attack)
        {
            return DealDamage(target, attack.power, attack.pierce, attack.isMagic, attack.element, attack.isMP, attack.effects);
        }

        public abstract void Die();

        /// <summary>
        /// Take an amount of damage of the specific element to HP or MP
        /// </summary>
        /// <param name="amount">The base amount of damage to take</param>
        /// <param name="element">The element being used (for UI stuff)</param>
        /// <param name="isMP">Is the damage being taken to MP?</param>
        public abstract int TakeDamage(int amount, Element element, bool isMP = false);
        

        /// <summary>
        /// Inflict one or more status effects on this entity. Negative chances remove the effect instead
        /// </summary>
        /// <param name="effect">The effect to inflict.</param>
        /// <param name="chance">The base chance to inflict this effect</param>
        public void ModifyStatus(StatusEffect effect, float chance)
        {
            float realChance = chance;
            //calculate status effect resistances if you're trying to inflict the effect
            if (chance > 0)
            {
                //establish lower bound of 0
                realChance = Math.Max(chance - this.stats.StatusResists[effect], 0);
            }
            //if there is no chance after applying resists, just leave
            if (realChance == 0) return;

            //if the chance is -100%, remove the effect
            if (realChance <= -1)
            {
                // &= ~effect is like (binary op) 110 & 101 = 100
                this.stats.currentEffects &= ~effect;
            }
            //else, try to roll to remove the effect
            else if (realChance < 0)
            {
                if (Random.Range(0,1) < Math.Abs(realChance))
                {
                    this.stats.currentEffects &= ~effect;
                }
            }
            //else, if the chance is 100%, apply the effect
            else if(realChance >= 1)
            {
                this.stats.currentEffects |= effect;
            }
            //otherwise, roll to try and inflict the effect
            else if (Random.Range(0, 1) < realChance)
            {
                this.stats.currentEffects |= effect;
            }
        }

    }
}
