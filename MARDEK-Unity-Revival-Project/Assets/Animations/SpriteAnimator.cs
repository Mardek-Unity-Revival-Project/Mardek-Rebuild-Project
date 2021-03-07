using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteAnimator : MonoBehaviour
{
    [SerializeField] float animationSpeed = 1f;
    [ExtendedSO]
    [SerializeField] SpriteAnimationClipList clipList = null;
    SpriteAnimationClip currentClip = null;

    float animationTimer = 0f;

    private void Start()
    {
        currentClip = clipList.GetClipByReference(null);
    }

    private void Update()
    {
        if (currentClip == null)
            return;

        animationTimer += animationSpeed * Time.deltaTime;
        while (animationTimer > 1)
            animationTimer -= 1;
        GetComponent<SpriteRenderer>().sprite = currentClip.GetSprite(animationTimer);
    }

    public void ChangeClipByReferecen(ScriptableObject reference)
    {
        currentClip = clipList.GetClipByReference(null);
    }
}
