using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerAnimation : MonoBehaviour
{
    [SerializeField] float flicksCyclesPerSecond = 1f;
    [SerializeField] Vector2 flickerAmplitude = .1f * Vector2.one;
    [SerializeField] bool smoothAnimation = true;

    Vector3 startingScale = Vector3.one;
    float timer = 0;

    private void Start()
    {
        startingScale = transform.localScale;
    }

    private void Update()
    {
        float flickerMultiplier;

        if (smoothAnimation)
            flickerMultiplier = Mathf.Sin(timer * flicksCyclesPerSecond * 2 * Mathf.PI);
        else
            flickerMultiplier = 1 - 2* Mathf.FloorToInt((flicksCyclesPerSecond * timer) % 2);

        Debug.Log(flickerMultiplier);

        Vector2 fickerAmout = flickerAmplitude * flickerMultiplier;
        Vector3 newScale = startingScale + (Vector3)fickerAmout;
        transform.localScale = newScale;

        timer += Time.deltaTime;
    }
}
