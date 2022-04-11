using MURP.SaveSystem;
using System.Collections.Generic;
using UnityEngine;

namespace MURP.DiscoverySystem
{
    public class ExploredAreas : AddressableMonoBehaviour
    {
        [SerializeField] Dictionary<string, ExploredArea> exploredAreas = new Dictionary<string, ExploredArea>();

        public bool MarkDiscovered(string sceneID, int tileX, int tileY)
        {
            if (!this.exploredAreas.ContainsKey(sceneID))
            {
                this.exploredAreas.Add(sceneID, new ExploredArea());
            }
            return this.exploredAreas[sceneID].MarkDiscovered(tileX, tileY);
        }

        public bool IsDiscovered(string sceneID, int tileX, int tileY)
        {
            ExploredArea area = null;
            this.exploredAreas.TryGetValue(sceneID, out area);
            
            return area != null && area.IsDiscovered(tileX, tileY);
        }
    }
}