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
        float animationRatio = 0f;

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
                    animationRatio += animationSpeed * Time.deltaTime;
                    bool endAnimation = !currentClip.loop && animationRatio > 1;
                    if (endAnimation)
                    {
                        isAnimating = false;
                        animationRatio = 0;
                    }
                    else
                    {
                        UpdateSprite(animationRatio);
                        while (animationRatio > 1)
                            animationRatio -= 1;
                    }
                }                
            }
        }

        void UpdateSprite(float _animationRatio)
        {
            if(currentClip != null)
                spriteRenderer.sprite = currentClip.GetSprite(_animationRatio);
        }

        public void StopCurrentAnimation(float forceAnimationRatio)
        {
            StopCurrentAnimation();
            animationRatio = forceAnimationRatio;
            UpdateSprite(animationRatio);
        }

        public void StopCurrentAnimation()
        {
            isAnimating = false;
        }

        public void PlayClipByMoveDirectionReference(MoveDirection reference)
        {
            SpriteAnimationClip nextClip = clipList.GetClipByReference(reference);
            currentClip = nextClip;
            isAnimating = true;                    
            animationRatio = 0;
        }
    }
}
