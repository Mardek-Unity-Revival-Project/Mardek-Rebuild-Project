using UnityEngine;
using MURP.Core;
using MURP.StatsSystem;

namespace MURP.Inventory
{
    [CreateAssetMenu(menuName = "MURP/Inventory/Item")]
    public class Item : AddressableScriptableObject
    {
        [SerializeField] string _displayName;
        [SerializeField] string _description;
        [SerializeField] Sprite _sprite;
        [SerializeField] Element _element;
        [SerializeField] int _price;

        Texture2D _readableSpriteTexture;

        void CreateReadableSpriteTexture()
        {
            RenderTexture tmp = RenderTexture.GetTemporary(
                this.sprite.texture.width,
                this.sprite.texture.height,
                0,
                RenderTextureFormat.ARGB32,
                RenderTextureReadWrite.Linear
            );
            Graphics.Blit(this.sprite.texture, tmp);

            RenderTexture previous = RenderTexture.active;
            RenderTexture.active = tmp;

            this._readableSpriteTexture = new Texture2D(this.sprite.texture.width, this.sprite.texture.height, TextureFormat.RGBA32, false);
            this._readableSpriteTexture.ReadPixels(new Rect(0, 0, tmp.width, tmp.height), 0, 0);
            this._readableSpriteTexture.Apply();

            RenderTexture.active = previous;
            RenderTexture.ReleaseTemporary(tmp);
        }

        public string displayName { get { return _displayName; } }

        public string description { get { return _description; } }

        public Sprite sprite { get { return _sprite; } }

        public Element element { get { return _element; } }

        public int price { get { return _price; } }

        public Texture2D readableSpriteTexture { get 
        { 
            if (this._readableSpriteTexture == null) this.CreateReadableSpriteTexture();
            return _readableSpriteTexture; 
        } }

        public virtual bool CanStack()
        {
            return true;
        }
    }
}