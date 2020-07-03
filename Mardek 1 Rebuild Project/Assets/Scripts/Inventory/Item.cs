using Assets.Scripts.NewFightingSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public abstract class Item : ScriptableObject
    {
        public string Name;
        public string Description;
        public Element Element;
        public int Value;
    }
}
