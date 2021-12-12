using UnityEngine;
using MURP.Core;

namespace MURP.Animation
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteAnimator : MonoBehaviour
    {
        [SerializeField] float animationSpeed = 1f;
        [SerializeField] bool _isAnimating = false;
        [SerializeField] SpriteAnimationClipList clipList = null;
        
        public bool isAnimating { get { return _isAnimating; } private set { _isAnimating = value; } }
        public bool currentClipLoops
        {
            get
            {
                if (currentClip == null)
                    return false;
                return currentClip.loop;
            }
        }
        
        SpriteAnimationClip currentClip = null;
        [HideInInspector] [SerializeField] SpriteRenderer spriteRenderer = null;
        float animationRatio = 0f;

        private void OnValidate()
        {
            InitializeFields();
        }

        private void Awake()
        {
            InitializeFields();
        }

        void InitializeFields()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            currentClip = clipList?.GetClipByIndex(0);
            if (currentClip == null)
                spriteRenderer.sprite = null;
            else
                if(spriteRenderer.sprite == null)
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
                            animationRatio = 0;
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
            if(reference == null)
            {
                StopCurrentAnimation(1);
                return;
            }
            SpriteAnimationClip nextClip = clipList.GetClipByReference(reference);
            currentClip = nextClip;
            isAnimating = true;                    
            animationRatio = 0;
        }
    }
}