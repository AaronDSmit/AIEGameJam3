using System.Collections;
using UnityEngine;

public class OnClickSquashAndStretch : MonoBehaviour
{
    [SerializeField]
    private float squashPercent;

    [SerializeField]
    private float stretchPercent;

    [SerializeField]
    private float animationTime;

    private RectTransform rect;

    private float startScaleX;
    private float startScaleY;

    private float stretchScaleX;
    private float stretchScaleY;

    private float squashScaleX;
    private float squashScaleY;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();

        startScaleX = rect.localScale.x;
        startScaleY = rect.localScale.y;

        squashScaleX = rect.localScale.x * squashPercent;
        squashScaleY = rect.localScale.y * squashPercent;

        stretchScaleX = rect.localScale.x * stretchPercent;
        stretchScaleY = rect.localScale.y * stretchPercent;
    }

    public void Click()
    {
        Debug.Log("on Click");

        StartCoroutine(SquashAndStretch());
    }

    private IEnumerator SquashAndStretch()
    {
        float currentLerpTime = 0.0f;
        float t = 0.0f;

        float targetScaleX;
        float targetScaleY;

        // Squash
        while (t < 1)
        {
            currentLerpTime += Time.deltaTime;
            t = currentLerpTime / (animationTime * 0.3f);
            t = 1f - Mathf.Cos(t * Mathf.PI * 0.5f);

            targetScaleX = Mathf.Lerp(startScaleX, squashScaleX, t);
            targetScaleY = Mathf.Lerp(startScaleY, squashScaleY, t);

            rect.localScale = new Vector3(targetScaleX, targetScaleY);

            yield return null;
        }

        currentLerpTime = 0;
        t = 0;

        // Stretch
        while (t < 1)
        {
            currentLerpTime += Time.deltaTime;
            t = currentLerpTime / (animationTime * 0.3f);
            t = 1f - Mathf.Cos(t * Mathf.PI * 0.5f);

            targetScaleX = Mathf.Lerp(squashScaleX, stretchScaleX, t);
            targetScaleY = Mathf.Lerp(squashScaleY, stretchScaleY, t);

            rect.localScale = new Vector3(targetScaleX, targetScaleY);

            yield return null;
        }

        currentLerpTime = 0;
        t = 0;

        // Stretch
        while (t < 1)
        {
            currentLerpTime += Time.deltaTime;
            t = currentLerpTime / (animationTime * 0.3f);
            t = 1f - Mathf.Cos(t * Mathf.PI * 0.5f);

            targetScaleX = Mathf.Lerp(stretchScaleX, startScaleX, t);
            targetScaleY = Mathf.Lerp(stretchScaleY, startScaleY, t);

            rect.localScale = new Vector3(targetScaleX, targetScaleY);

            yield return null;
        }
    }
}