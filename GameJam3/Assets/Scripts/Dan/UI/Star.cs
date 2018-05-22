using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Star : MonoBehaviour {
    [SerializeField] private Image empty;
    [SerializeField] private Image filled;
    [SerializeField] private Image outline;
    [SerializeField] private Image glow;

    private Vector2 originalSize;

    private void Start () {
        originalSize = filled.rectTransform.localScale;
    }

    public void ActivateStar (StarActivation activationInfo) {
        StartCoroutine(Activate(activationInfo));
    }

    public void ResetStar () {
		if(originalSize == Vector2.zero)
			originalSize = filled.rectTransform.localScale;

		filled.rectTransform.localScale = Vector2.zero;

		Color c = outline.color;
        c.a = 0f;

        outline.color = c;

        c = glow.color;
        c.a = 0f;

        glow.color = c;

        filled.gameObject.SetActive(false);
        glow.gameObject.SetActive(false);
    }

    private IEnumerator Activate (StarActivation activationInfo) {
        float t = 0f;

		originalSize = Vector2.one;

		filled.gameObject.SetActive(true);
        glow.gameObject.SetActive(true);
        Vector2 startSize = Vector2.zero;

        while (t <= activationInfo.ScaleTime) {
            t += Time.deltaTime;

            filled.rectTransform.localScale = Vector2.LerpUnclamped(startSize, originalSize, 
                activationInfo.ScaleCurve.Evaluate(t/activationInfo.ScaleTime));

            yield return null;
        }

        filled.rectTransform.sizeDelta = originalSize;

        t = 0;

        Color startColour = outline.color;
        Color endColour = startColour;
        endColour.a = 1f;

        while (t <= activationInfo.BorderFadeTime) {
            t += Time.deltaTime;

            outline.color = Color.Lerp(startColour, endColour, t / activationInfo.BorderFadeTime);

            yield return null;
        }

        outline.color = endColour;

        t = 0;

        Vector2 originalScale = filled.rectTransform.localScale;
        Vector2 endIncreaseScale = originalScale;
        endIncreaseScale.x += activationInfo.IncreaseScale;
        endIncreaseScale.y += activationInfo.IncreaseScale;


        while (t <= activationInfo.IncreaseScaleTime) {
            t += Time.deltaTime;

            filled.rectTransform.localScale = Vector2.LerpUnclamped(originalScale, endIncreaseScale, 
                activationInfo.IncreaseCurve.Evaluate(t / activationInfo.IncreaseScaleTime));

            outline.rectTransform.localScale = Vector2.LerpUnclamped(originalScale, endIncreaseScale,
                activationInfo.IncreaseCurve.Evaluate(t / activationInfo.IncreaseScaleTime));

            yield return null;
        }

        t = 0f;

        startColour = glow.color;
        endColour = startColour;
        endColour.a = 1f;

        while(t <= activationInfo.GlowFadeTime) {
            t += Time.deltaTime;

            glow.color = Color.Lerp(startColour, endColour, t / activationInfo.GlowFadeTime);

            yield return null;
        }

        glow.color = endColour;
    }
}
