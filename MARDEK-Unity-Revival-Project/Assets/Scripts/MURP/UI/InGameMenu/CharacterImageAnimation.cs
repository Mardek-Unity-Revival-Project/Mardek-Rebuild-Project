using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MURP.Core;
using MURP.CharacterSystem;
using UnityEngine.UI;

namespace MURP.UI
{
    public class CharacterImageAnimation : MonoBehaviour
    {
        [SerializeField] CharacterSelectable characterSelectable;
        [SerializeField] Image characterImage;
        [SerializeField] MoveDirection movementSpriteAnimationDirection;

        public void Update()
        {
            var characterInfo = characterSelectable.Character.CharacterInfo;
            if (characterInfo == null)
                return;
            var clip = characterInfo.WalkSprites.GetClipByReference(movementSpriteAnimationDirection);
            var animRatio = Time.time % 1;
            characterImage.sprite = clip.GetSprite(animRatio);
        }
    }
}