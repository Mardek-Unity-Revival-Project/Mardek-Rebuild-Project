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
            if (currentClip == null)
                return;

            animationTimer += animationSpeed * Time.deltaTime;
            while (animationTimer > 1)
                animationTimer -= 1;
            spriteRenderer.sprite = currentClip.GetSprite(animationTimer);
        }

        public void ChangeClipByReferecen(MoveDirection reference)
        {
            currentClip = clipList.GetClipByReference(reference);
        }
    }
}
