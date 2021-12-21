using UnityEngine;

public class FullscreenSprite : MonoBehaviour {
    
    void Awake()
    {
        ResizeSprite();
    }

    int lastWidth = -1;
    int lastHeight = -1;
    int resizeCountdown;

    void FixedUpdate()
    {
        int currentWidth = Camera.main.pixelWidth;
        int currentHeight = Camera.main.pixelHeight;
        
        if (currentWidth != lastWidth || currentHeight != lastHeight)
        {
            lastWidth = currentWidth;
            lastHeight = currentHeight;
            resizeCountdown = 10;
        }
        else if (resizeCountdown > 0)
        {
            resizeCountdown--;
            if (resizeCountdown == 0)
            {
                ResizeSprite();
            }
        }
    }

    void ResizeSprite()
    {
        Vector2 cameraSize = new Vector2(Camera.main.pixelWidth / 1f, Camera.main.pixelHeight / 1f);
        
        float hardAspect = 1920f / 1080f;
        float cameraAspect = cameraSize.x / cameraSize.y;
        float combinedAspect = cameraAspect / hardAspect;
        if (combinedAspect < 1f) combinedAspect = 1f / combinedAspect;

        transform.localScale = new Vector2(combinedAspect, combinedAspect);
    }
}
