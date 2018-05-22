using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlideInUI : MonoBehaviour
{
    [SerializeField]
    private float targetX;

    [SerializeField]
    private float overshootTargetX;

    [SerializeField]
    private float animationTime;

    private RectTransform rect;

    private Button[] buttons;

    private float startX;

    private bool pulledDown = false;

    public bool PulledDown
    {
        get { return pulledDown; }
    }

    private void Awake()
    {
        rect = GetComponent<RectTransform>();

        startX = rect.anchoredPosition.x;

        RectTransform[] rects = GetComponentsInChildren<RectTransform>();

        buttons = GetComponentsInChildren<Button>();
    }

    public void TogglePullDown()
    {
        if (!PulledDown)
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

        if (buttons.Length > 0)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].interactable = pulledDown;
            }
        }
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

        if (buttons.Length > 0)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].interactable = pulledDown;
            }
        }
    }
}