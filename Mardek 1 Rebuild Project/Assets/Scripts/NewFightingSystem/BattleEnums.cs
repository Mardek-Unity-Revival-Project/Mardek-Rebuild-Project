using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.NewFightingSystem
{
    /// <summary>
    /// The element enum defines the 9 elements used during gameplay
    /// </summary>
    [Flags]
    public enum Element
    {
        Divine = 0, Physical = 1, Thauma = 2, Light = 4, Dark = 8, Air = 16, Earth = 32, Water = 64, Fire = 128, Aether = 256, Fig = 512
    }

    /// <summary>
    /// The Statistic enum defines the various statistics that can be modified during battle
    /// </summary>
    public enum Statistic
    {
        HP, MP, STR, AGI, VIT, SPR, DEF, MDEF
    }

    /// <summary>
    /// The Status Effect enum defines all the status effects that can be applied to an entity.
    /// </summary>
    /// <remarks>The Remedy effect can be invoked by performing an &= 1023 on the currentEffects property for an entity</remarks>
    [Flags]
    public enum StatusEffect
    {
        None = 0, Poison = 1, Paralysis = 2, Numb, Silence = 8, Curse = 16, Sleep = 32, Confusion = 64, Blind = 128,
        Bleed = 256, Zombie = 512, Shield = 1024, MShield = 2048, Regen = 4096, Haste = 8192, Berserk = 16384
    }
}
