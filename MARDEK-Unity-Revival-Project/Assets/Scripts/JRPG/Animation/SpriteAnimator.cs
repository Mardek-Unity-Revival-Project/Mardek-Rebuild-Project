using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JRPG
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteAnimator : MonoBehaviour
    {
        [SerializeField] float animationSpeed = 1f;
        [ExtendedSO]
        [SerializeField] SpriteAnimationClipList clipList = null;
        
        SpriteAnimationClip currentClip = null;
        SpriteRenderer spriteRenderer = null;

        public bool isAnimating { get; private set; }
        float animationTimer = 0f;

        private void Awake()
        {
            currentClip = clipList.GetClipByReference(null);
        }

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

        public void StopCurrentAnimation()
        {
            Debug.Log("stop");
            isAnimating = false;
        }

        public void PlayClipByMoveDirectionReference(MoveDirection reference)
        {
            Debug.Log("play");

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
