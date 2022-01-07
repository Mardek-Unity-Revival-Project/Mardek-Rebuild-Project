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
        Vector2 cameraSize = new Vector2(Camera.main.pixelWidth, Camera.main.pixelHeight);
        float hardAspect = 1920f / 1080f;
        float cameraAspect = cameraSize.x / cameraSize.y;

        // Requirements:
        // (1) No distortion -> scaleX == scaleY
        // (2) Background image must be at least as wide as the camera -> cameraSize.y * hardAspect * scaleX >= cameraSize.x
        // (3) Background image must be at leats as high as the camera -> scaleY >= 1
        // (4) The scale should not be larger than neccessary -> pick minimum required scale
        //
        // From (2) it follows that scaleX >= cameraSize.x / cameraSize.y / hardAspect = cameraAspect / hardAspect

        float scale = Mathf.Max(cameraAspect / hardAspect, 1f);

        transform.localScale = new Vector2(scale, scale);
    }
}
