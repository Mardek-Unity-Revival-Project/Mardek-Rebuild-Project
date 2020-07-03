using Assets.Scripts.Entities;
using Assets.Scripts.NewFightingSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// The Character class defines a controllable player character, with a name, stats, and an inventory
    /// </summary>
    public class Character : Entity
    {
        public Item[] Inventory = new Item[40];
        /// <summary>
        /// Is the character required to be in the party?
        /// </summary>
        public bool IsNecessary;
    }
}
