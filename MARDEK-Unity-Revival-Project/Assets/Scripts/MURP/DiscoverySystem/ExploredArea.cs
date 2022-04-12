using System.Collections.Generic;
using UnityEngine;

namespace MURP.DiscoverySystem
{
    public class ExploredArea
    {
        [SerializeField] List<Vector2> discoveredTiles = new List<Vector2>();

        public void MarkDiscovered(int tileX, int tileY)
        {
            if (!IsDiscovered(tileX, tileY))
            {
                discoveredTiles.Add(new Vector2(tileX, tileY));
            }
        }

        public bool IsDiscovered(int tileX, int tileY)
        {
            return discoveredTiles.Contains(new Vector2(tileX, tileY));
        }
    }
}
