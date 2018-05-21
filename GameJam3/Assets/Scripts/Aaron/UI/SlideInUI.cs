using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideInUI : MonoBehaviour
{
    [SerializeField]
    private float targetX;

    [SerializeField]
    private float overshootTargetX;

    [SerializeField]
    private float animationTime;

    RectTransform rect;

    private float startX;

    private bool pulledDown = false;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();

        startX = rect.anchoredPosition.x;

        RectTransform[] rects = GetComponentsInChildren<RectTransform>();
    }

    public void Start()
    {
        TogglePullDown();
    }

    public void TogglePullDown()
    {
        if (!pulledDown)
        {
            StartCoroutine(AnimatePullDown(startX, overshootTargetX, targetX));
        }
        else
        {
            StartCoroutine(AnimatePullUp(targetX, overshootTargetX, startX));
        }
    }

    private IEnumerator AnimatePullDown(float from, float overshoot, float targetY)
    {
        float currentLerpTime = 0;
        float t = 0;

        while (t < 1)
        {
            currentLerpTime += Time.deltaTime;
            t = currentLerpTime / (animationTime * 0.7f);
            t = t * t;

            rect.anchoredPosition = new Vector3(Mathf.Lerp(from, overshoot, t), rect.anchoredPosition.y);

            yield return null;
        }

        currentLerpTime = 0;
        t = 0;

        while (t < 1)
        {
            currentLerpTime += Time.deltaTime;
            t = currentLerpTime / (animationTime * 0.3f);

            rect.anchoredPosition = new Vector3(Mathf.Lerp(overshoot, targetY, t), rect.anchoredPosition.y);

            yield return null;
        }


        pulledDown = !pulledDown;
    }

    private IEnumerator AnimatePullUp(float from, float overshoot, float targetY)
    {
        float currentLerpTime = 0;
        float t = 0;

        while (t < 1)
        {
            currentLerpTime += Time.deltaTime;
            t = currentLerpTime / (animationTime * 0.3f);
            t = 1f - Mathf.Cos(t * Mathf.PI * 0.5f);

            rect.anchoredPosition = new Vector3(Mathf.Lerp(from, overshoot, t), rect.anchoredPosition.y);

            yield return null;
        }

        currentLerpTime = 0;
        t = 0;

        while (t < 1)
        {
            currentLerpTime += Time.deltaTime;
            t = currentLerpTime / (animationTime * 0.7f);
            t = t * t;

            rect.anchoredPosition = new Vector3(Mathf.Lerp(overshoot, targetY, t), rect.anchoredPosition.y);

            yield return null;
        }


        pulledDown = !pulledDown;
    }
}