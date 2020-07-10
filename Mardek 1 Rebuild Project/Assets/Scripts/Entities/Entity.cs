using Assets.Scripts.Inventory;
using Assets.Scripts.NewFightingSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Entities
{
    /// <summary>
    /// The Entity class is the base class for all player characters, monsters, etc.
    /// </summary>
    public abstract class Entity : MonoBehaviour
    {
        public string Name;
        public Stats stats;
        public Element Element;
        public Equipment[] Equipment = new Equipment[6];
    }
}
