using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerAnimation : MonoBehaviour
{
    [SerializeField] float flicksPerSecond = 1f;
    [SerializeField] Vector2 flickerAmplitude = .1f * Vector2.one;
    Vector3 startingScale = Vector3.one;
    float timer = 0;

    private void Start()
    {
        startingScale = transform.localScale;
    }

    private void Update()
    {
        float flickerMultiplier = Mathf.Sin(timer * flicksPerSecond * 2 * Mathf.PI);
        Vector2 fickerAmout = flickerAmplitude * flickerMultiplier;
        Vector3 newScale = startingScale + (Vector3)fickerAmout;
        transform.localScale = newScale;

        timer += Time.deltaTime;
    }
}
