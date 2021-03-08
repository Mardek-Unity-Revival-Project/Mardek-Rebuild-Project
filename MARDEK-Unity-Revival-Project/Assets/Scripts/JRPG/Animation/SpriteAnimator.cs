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

        float animationTimer = 0f;

        private void Awake()
        {
            currentClip = clipList.GetClipByReference(null);
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            animationTimer += animationSpeed * Time.deltaTime;
            while (animationTimer > 1)
                animationTimer -= 1;
            UpdateSprite();
        }

        void UpdateSprite()
        {
            if(currentClip != null)
                spriteRenderer.sprite = currentClip.GetSprite(animationTimer);
        }

        public void ChangeClipByReferecen(MoveDirection reference)
        {
            SpriteAnimationClip nextClip = clipList.GetClipByReference(reference);
            if(currentClip != nextClip)
                animationTimer = 0;
            currentClip = nextClip;
            UpdateSprite();
        }
    }
}
