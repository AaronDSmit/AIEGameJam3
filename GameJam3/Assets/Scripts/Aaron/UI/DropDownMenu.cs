using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropDownMenu : MonoBehaviour
{
    [SerializeField]
    private float targetY;

    [SerializeField]
    private float overshootTargetY;

    [SerializeField]
    private float animationTime;

    RectTransform rect;

    private float startY;

    private bool pulledDown = false;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();

        startY = rect.anchoredPosition.y;

        RectTransform[] rects = GetComponentsInChildren<RectTransform>();
    }

    private void Start()
    {
        TogglePullDown();
    }

    public void TogglePullDown()
    {
        if (!pulledDown)
        {
            StartCoroutine(AnimatePullDown(startY, overshootTargetY, targetY));
        }
        else
        {
            StartCoroutine(AnimatePullUp(targetY, overshootTargetY, startY));
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

            rect.anchoredPosition = new Vector3(rect.anchoredPosition.x, Mathf.Lerp(from, overshoot, t));

            yield return null;
        }

        currentLerpTime = 0;
        t = 0;

        while (t < 1)
        {
            currentLerpTime += Time.deltaTime;
            t = currentLerpTime / (animationTime * 0.3f);

            rect.anchoredPosition = new Vector3(rect.anchoredPosition.x, Mathf.Lerp(overshoot, targetY, t));

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

            rect.anchoredPosition = new Vector3(rect.anchoredPosition.x, Mathf.Lerp(from, overshoot, t));

            yield return null;
        }

        currentLerpTime = 0;
        t = 0;

        while (t < 1)
        {
            currentLerpTime += Time.deltaTime;
            t = currentLerpTime / (animationTime * 0.7f);
            t = t * t;

            rect.anchoredPosition = new Vector3(rect.anchoredPosition.x, Mathf.Lerp(overshoot, targetY, t));

            yield return null;
        }


        pulledDown = !pulledDown;
    }
}