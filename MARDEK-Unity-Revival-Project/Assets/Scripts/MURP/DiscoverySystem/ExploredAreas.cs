using MURP.Core;
using MURP.MovementSystem;
using MURP.SaveSystem;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MURP.DiscoverySystem
{
    public class ExploredAreas : AddressableMonoBehaviour
    {
        [SerializeField] Dictionary<string, ExploredArea> exploredAreas = new Dictionary<string, ExploredArea>();
        [SerializeField] PlayerController player;

        int counter = 0;

        public void MarkDiscovered(string sceneID, int tileX, int tileY)
        {
            if (!this.exploredAreas.ContainsKey(sceneID))
            {
                this.exploredAreas.Add(sceneID, new ExploredArea());
            }
            this.exploredAreas[sceneID].MarkDiscovered(tileX, tileY);
        }

        public bool IsDiscovered(string sceneID, int tileX, int tileY)
        {
            ExploredArea area = null;
            this.exploredAreas.TryGetValue(sceneID, out area);
            
            return area != null && area.IsDiscovered(tileX, tileY);
        }

        void FixedUpdate()
        {
            counter += 1;
            if (counter % 50 == 0)
            {
                MarkAreaAroundPlayerAsDiscovered();
                counter = 0;
            }
        }

        void MarkAreaAroundPlayerAsDiscovered()
        {
            Scene activeScene = SceneManager.GetActiveScene();
            SceneInfo sceneInfo = null;

            foreach (GameObject gameObject in activeScene.GetRootGameObjects())
            {
                sceneInfo = gameObject.GetComponent<SceneInfo>();
                if (sceneInfo != null)
                {
                    break;
                }
            }

            int playerX = (int) player.transform.position.x;
            int playerY = (int) player.transform.position.y;

            // The original game used also used a discover radius of 9 tiles
            int discoverRadius = 9;
            for (int x = playerX - discoverRadius; x <= playerX + discoverRadius; x++)
            {
                for (int y = playerY - discoverRadius; y <= playerY + discoverRadius; y++)
                {
                    MarkDiscovered(sceneInfo.id, x, y);
                }
            }
        }
    }
}