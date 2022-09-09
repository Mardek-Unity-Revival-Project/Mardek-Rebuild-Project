using MURP.Core;
using MURP.DiscoverySystem;
using MURP.MovementSystem;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

namespace MURP.UI
{
    public class Map : MonoBehaviour
    {
        static readonly Color UNEXPLORED_COLOR = new Color(48f / 255f, 34f / 255f, 20f / 255f);
        static readonly Color PASSABLE_TERRAIN_COLOR = new Color(128f / 255f, 110f / 255f, 78f / 255f);
        static readonly Color IMPASSABLE_TERRAIN_COLOR = new Color(78f / 255f, 51f / 255f, 33f / 255f);
        static readonly Color INTERACT_COLOR = new Color(0f, 1f, 0f);
        static readonly Color SAVE_CRYSTAL_COLOR = new Color(0f, 1f, 1f);
        static readonly Color UNOPENED_CHEST_COLOR = new Color(0.9f, 0.7f, 0f);
        static readonly Color OPENED_CHEST_COLOR = new Color(0.9f, 0.3f, 0f);
        static readonly Color PEOPLE_COLOR = new Color(1f, 0f, 0f);
        static readonly Color DOOR_COLOR = new Color(1f, 1f, 1f);
        static readonly Color PLAYER_COLOR = new Color(0.8f, 0.1f, 0.7f);

        [SerializeField] Text activeSceneName;
        [SerializeField] Image mapImage;
        [SerializeField] ExploredAreas exploredAreas;

        string sceneID;
        Sprite baseSprite;
        Sprite flickerSprite;

        int counter;

        void FixedUpdate() {
            this.counter += 1;
            if (this.counter % 40 == 0)
            {
                this.mapImage.sprite = this.flickerSprite;
                this.counter = 0;
            }
            else if (this.counter % 20 == 0) 
            {
                this.mapImage.sprite = this.baseSprite;
            }
        }

        Color DetermineColor(Tilemap tilemap, List<Collider2D> otherColliders, int x, int y, bool useMoreColors, bool ignoreExploration)
        {
            if (!ignoreExploration && !this.exploredAreas.IsDiscovered(this.sceneID, x, y)) return UNEXPLORED_COLOR;

            foreach (Collider2D otherCollider in otherColliders)
            {
                if (otherCollider.OverlapPoint(new Vector2(x + 0.5f, y + 0.5f)))
                {
                    // TODO Other interactable objects
                    if (otherCollider.gameObject.GetComponent<SaveSystem.SaveSystem>()) return useMoreColors ? SAVE_CRYSTAL_COLOR : PASSABLE_TERRAIN_COLOR;
                    // TODO Chests
                    // TODO People
                    if (otherCollider.gameObject.GetComponent<SceneTransitionCommand>()) return useMoreColors ? DOOR_COLOR : PASSABLE_TERRAIN_COLOR;
                    if (otherCollider.gameObject.GetComponent<PlayerController>()) return useMoreColors ? PLAYER_COLOR : PASSABLE_TERRAIN_COLOR;
                    return IMPASSABLE_TERRAIN_COLOR;
                }
            }

            TileBase tileBase = tilemap.GetTile(new Vector3Int(x, y, 0));
            if (tileBase is Tile) {
                Tile tile = tileBase as Tile;
                if (tile.colliderType == Tile.ColliderType.None) return PASSABLE_TERRAIN_COLOR;
                else return IMPASSABLE_TERRAIN_COLOR;
            } 
            else 
            {
                return IMPASSABLE_TERRAIN_COLOR;
            }
        }

        

        void UpdateSprites()
        {
            Scene activeScene = SceneManager.GetActiveScene();

            Tilemap tilemap = null;
            List<Collider2D> otherColliders = new List<Collider2D>();
            SceneInfo sceneInfo = null;

            foreach (GameObject gameObject in activeScene.GetRootGameObjects()) {
                if (gameObject.name.Equals("Grid")) {
                    tilemap = gameObject.GetComponentInChildren<Tilemap>();
                }
                foreach (Collider2D collider in gameObject.GetComponentsInChildren<Collider2D>()) {
                    if (!collider.gameObject.GetComponent<Tilemap>() && !collider.gameObject.GetComponent<FollowMovementController>())
                    {
                        otherColliders.Add(collider);
                    }
                }
                if (sceneInfo == null) {
                    sceneInfo = gameObject.GetComponent<SceneInfo>();
                }
            }

            if (sceneInfo == null)
            {
                throw new System.ApplicationException("Scene " + activeScene.name + " doesn't have a SceneInfo root component");
            }
            this.activeSceneName.text = sceneInfo.displayName;
            this.sceneID = sceneInfo.id;

            // Find the relevant part of the map (the smallest rectangle that contains all passable terrain and all chests, people, etc)
            int minX = 1000;
            int minY = 1000;
            int maxX = -1000;
            int maxY = -1000;

            for (int x = tilemap.cellBounds.xMin; x <= tilemap.cellBounds.xMax; x++)
            {
                for (int y = tilemap.cellBounds.yMin; y <= tilemap.cellBounds.yMax; y++)
                {
                    Color color = this.DetermineColor(tilemap, otherColliders, x, y, true, true);
                    bool isInteresting = color != IMPASSABLE_TERRAIN_COLOR;

                    if (isInteresting)
                    {
                        if (x < minX) minX = x;
                        if (x > maxX) maxX = x;
                        if (y < minY) minY = y;
                        if (y > maxY) maxY = y;
                    }
                }
            }

            int width = 1 + maxX - minX;
            int height = 1 + maxY - minY;

            Texture2D baseMapTexture = new Texture2D(width, height);
            Texture2D flickerMapTexture = new Texture2D(width, height);
            baseMapTexture.filterMode = FilterMode.Point;
            flickerMapTexture.filterMode = FilterMode.Point;
            for (int textureX = 0; textureX < width; textureX++)
            {
                for (int textureY = 0; textureY < height; textureY++)
                {
                    int tileX = textureX + minX;
                    int tileY = textureY + minY;
                    baseMapTexture.SetPixel(textureX, textureY, this.DetermineColor(tilemap, otherColliders, tileX, tileY, false, false));
                    flickerMapTexture.SetPixel(textureX, textureY, this.DetermineColor(tilemap, otherColliders, tileX, tileY, true, false));
                }
            }
            baseMapTexture.Apply();
            flickerMapTexture.Apply();

            this.baseSprite = Sprite.Create(baseMapTexture, new Rect(0, 0, width, height), Vector2.zero);
            this.flickerSprite = Sprite.Create(flickerMapTexture, new Rect(0, 0, width, height), Vector2.zero);
            this.mapImage.sprite = this.flickerSprite;

            int sizePerTile = 12;
            this.mapImage.rectTransform.sizeDelta = new Vector2(width * sizePerTile, height * sizePerTile);
        }

        void OnEnable()
        {
            UpdateSprites();
        }
    }
}