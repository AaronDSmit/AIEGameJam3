using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarAnimationVertical : MonoBehaviour
{
    [SerializeField]
    private float maxHeight;

    [SerializeField]
    private float animationTime;

    private RectTransform valueRect;
    private RectTransform ghostRect;

    private float currentValuePercent;
    private float currentGhostPercent;

    private void Awake()
    {
        valueRect = transform.GetChild(1).GetComponent<RectTransform>();
        ghostRect = transform.GetChild(0).GetComponent<RectTransform>();

        currentValuePercent = 0.0f;
        currentGhostPercent = 0.0f;
    }

    public void UpdateValue(float valuePercent)
    {
        if (currentValuePercent > valuePercent)
        {
            // increase Value to ghost level

            StartCoroutine(AnimateGhostToTargetValue(valuePercent));
        }
        else
        {
            // decrease ghost level to value

            StartCoroutine(AnimateValueBack(valuePercent));
        }
    }

    private IEnumerator AnimateValueToGhost()
    {
        float currentLerpTime = 0;
        float t = 0;

        while (t < 1)
        {
            currentLerpTime += Time.deltaTime;
            t = currentLerpTime / (animationTime);

            t = 1f - Mathf.Cos(t * Mathf.PI * 0.5f);

            valueRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Mathf.Lerp(currentValuePercent * maxHeight, currentGhostPercent * maxHeight, t));

            yield return null;
        }

        currentValuePercent = currentGhostPercent;
    }

    private IEnumerator AnimateValueBack(float targetValue)
    {
        float currentLerpTime = 0;
        float t = 0;

        while (t < 1)
        {
            currentLerpTime += Time.deltaTime;
            t = currentLerpTime / (animationTime);

            t = 1f - Mathf.Cos(t * Mathf.PI * 0.5f);

            valueRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Mathf.Lerp(currentValuePercent * maxHeight, targetValue * maxHeight, t));

            yield return null;
        }

        currentValuePercent = targetValue;

        StartCoroutine(AnimateGhostToValue());
    }

    private IEnumerator AnimateGhostToTargetValue(float targetValue)
    {
        float currentLerpTime = 0;
        float t = 0;

        while (t < 1)
        {
            currentLerpTime += Time.deltaTime;

            t = currentLerpTime / (animationTime);
            t = 1f - Mathf.Cos(t * Mathf.PI * 0.5f);

            ghostRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Mathf.Lerp(currentGhostPercent * maxHeight, targetValue * maxHeight, t));

            yield return null;
        }

        currentGhostPercent = targetValue;

        StartCoroutine(AnimateValueToGhost());
    }

    private IEnumerator AnimateGhostToValue()
    {
        float currentLerpTime = 0;
        float t = 0;

        while (t < 1)
        {
            currentLerpTime += Time.deltaTime;

            t = currentLerpTime / (animationTime);
            t = 1f - Mathf.Cos(t * Mathf.PI * 0.5f);

            ghostRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Mathf.Lerp(currentGhostPercent * maxHeight, currentValuePercent * maxHeight, t));

            yield return null;
        }

        currentGhostPercent = currentValuePercent;
    }
}