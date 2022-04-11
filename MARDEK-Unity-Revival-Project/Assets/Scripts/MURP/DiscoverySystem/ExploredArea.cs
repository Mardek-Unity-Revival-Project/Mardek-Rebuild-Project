using UnityEngine;

namespace MURP.DiscoverySystem
{
    public class ExploredArea
    {
        // Note: these default values will guarantee that ResizeIfNeeded will initialize the area upon the first call
        [SerializeField] int minX = 1;
        [SerializeField] int minY;
        [SerializeField] int maxX = -1;
        [SerializeField] int maxY;

        [SerializeField] bool[][] discoveredTiles;

        private void ResizeIfNeeded(int newTileX, int newTileY)
        {
            // If this is the case, this ExploredArea hasn't been initialized yet
            if (this.minX > this.maxX)
            {
                this.minX = newTileX;
                this.minY = newTileY;
                this.maxX = newTileX;
                this.maxY = newTileY;
                this.discoveredTiles = new bool[1][];
                this.discoveredTiles[0] = new bool[1];
                return;
            }

            if (newTileX < this.minX || newTileX > this.maxX)
            {
                int newMinX = newTileX < this.minX ? newTileX : this.minX;
                int newMaxX = newTileX > this.maxX ? newTileX : this.maxX;
                bool[][] newDiscoveredTiles = new bool[this.discoveredTiles.Length + 1][];
                for (int x = newMinX; x <= newMaxX; x++)
                {
                    if (x >= this.minX && x <= this.maxX)
                    {
                        newDiscoveredTiles[x - newMinX] = this.discoveredTiles[x - this.minX];
                    }
                    else
                    {
                        newDiscoveredTiles[x - newMinX] = new bool[1 + this.maxY - this.minY];
                    }
                }

                this.minX = newMinX;
                this.maxX = newMaxX;
                this.discoveredTiles = newDiscoveredTiles;
            }

            if (newTileY < this.minY || newTileY > this.maxY)
            {
                int newMinY = newTileY < this.minY ? newTileY : this.minY;
                int newMaxY = newTileY > this.maxY ? newTileY : this.maxY;

                for (int x = this.minX; x <= this.maxX; x++)
                {
                    bool[] oldColumn = this.discoveredTiles[x - this.minX];
                    bool[] newColumn = new bool[1 + newMaxY - newMinY];
                    for (int y = newMinY; y <= newMaxY; y++)
                    {
                        if (y >= this.minY && y <= this.maxY)
                        {
                            newColumn[y - newMinY] = oldColumn[y - this.minY];
                        }
                    }
                    this.discoveredTiles[x - this.minX] = newColumn;
                }

                this.minY = newMinY;
                this.maxY = newMaxY;
            }
        }

        public bool MarkDiscovered(int tileX, int tileY)
        {
            this.ResizeIfNeeded(tileX, tileY);
            
            bool wasDiscovered = this.discoveredTiles[tileX - this.minX][tileY - this.minY];
            this.discoveredTiles[tileX - this.minX][tileY - this.minY] = true;
            return wasDiscovered;
        }

        public bool IsDiscovered(int tileX, int tileY)
        {
            if (tileX >= this.minX && tileX <= this.maxX && tileY >= this.minY && tileY <= this.maxY)
            {
                return this.discoveredTiles[tileX - this.minX][tileY - this.minY];
            }
            else
            {
                return false;
            }
        }
    }
}