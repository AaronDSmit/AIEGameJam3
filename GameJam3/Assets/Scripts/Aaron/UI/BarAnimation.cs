using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarAnimation : MonoBehaviour
{
    [SerializeField]
    private float maxWidth;

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
        if (currentValuePercent < valuePercent)
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

            valueRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Mathf.Lerp(currentValuePercent * maxWidth, currentGhostPercent * maxWidth, t));

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

            valueRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Mathf.Lerp(currentValuePercent * maxWidth, targetValue * maxWidth, t));

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

            ghostRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Mathf.Lerp(currentGhostPercent * maxWidth, targetValue * maxWidth, t));

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

            ghostRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Mathf.Lerp(currentGhostPercent * maxWidth, currentValuePercent * maxWidth, t));

            yield return null;
        }

        currentGhostPercent = currentValuePercent;
    }
}