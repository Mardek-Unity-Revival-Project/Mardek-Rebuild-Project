using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JRPG
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteAnimator : MonoBehaviour
    {
        [SerializeField] float animationSpeed = 1f;
        [SerializeField] bool _isAnimating = false;
        [SerializeField] SpriteAnimationClipList clipList = null;
        
        public bool isAnimating { get { return _isAnimating; } private set { _isAnimating = value; } }
        
        SpriteAnimationClip currentClip = null;
        SpriteRenderer spriteRenderer = null;
        float animationTimer = 0f;

        private void OnValidate()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            currentClip = clipList?.GetClipByIndex(0);
            if (currentClip == null)
                spriteRenderer.sprite = null;
            else
                UpdateSprite(0);
        }

        private void Update()
        {
            if (currentClip != null)
            {
                if (isAnimating)
                {
                    animationTimer += animationSpeed * Time.deltaTime;
                    bool endAnimation = !currentClip.loop && animationTimer > 1;
                    if(endAnimation)
                        isAnimating = false;
                    else
                    {
                        while (animationTimer > 1)
                            animationTimer -= 1;
                        UpdateSprite(animationTimer);
                    }
                }                
            }
        }

        void UpdateSprite(float animationRatio)
        {
            if(currentClip != null)
                spriteRenderer.sprite = currentClip.GetSprite(animationRatio);
        }

        public void StopCurrentAnimation(bool resetToFirstSprite)
        {
            isAnimating = false;
            if(resetToFirstSprite)
                UpdateSprite(0);
        }

        public void PlayClipByMoveDirectionReference(MoveDirection reference)
        {
            SpriteAnimationClip nextClip = clipList.GetClipByReference(reference);

            if(nextClip != null)
            {
                bool resetAnim = nextClip != currentClip;
                currentClip = nextClip;
                isAnimating = true;
                if(resetAnim)
                    UpdateSprite(0);
            }
        }
    }
}
