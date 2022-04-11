using System.Collections.Generic;
using UnityEngine;

namespace MURP.DiscoverySystem
{
    public class ExploredArea
    {
        [SerializeField] List<Vector2> discoveredTiles;

        public bool MarkDiscovered(int tileX, int tileY)
        {
            if (IsDiscovered(tileX, tileY))
            {
                return true;
            } else {
                if (discoveredTiles == null)
                {
                    discoveredTiles = new List<Vector2>();
                }
                discoveredTiles.Add(new Vector2(tileX, tileY));
                return false;
            }
        }

        public bool IsDiscovered(int tileX, int tileY)
        {
            return discoveredTiles != null && discoveredTiles.Contains(new Vector2(tileX, tileY));
        }
    }
}
