using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarRatingUI : MonoBehaviour {
    [SerializeField] private RectTransform endOfDayStarParent;
	[SerializeField] private RectTransform finalDayStarParent;
    [SerializeField] private StarActivation activationInfo;
    [SerializeField] private float delayBetweenStars;

    private List<Star> endOfDayStars;
	private List<Star> finalDayStars;

    private void Start () {
        endOfDayStars = new List<Star>();
		finalDayStars = new List<Star>();

        foreach (Transform child in endOfDayStarParent.transform) {
            endOfDayStars.Add(child.GetComponent<Star>());
        }

		foreach (Transform child in finalDayStarParent.transform) {
			finalDayStars.Add(child.GetComponent<Star>());
		}
	}

    public void ActivateEndOfDayStars (StarRating rating, float delay) {
        for (int i = 0; i < endOfDayStars.Count; i++) {
            endOfDayStars[i].ResetStar();
        }

        switch (rating) {
            case StarRating.ZERO:
                break;
            case StarRating.ONE:
                StartCoroutine(Activate(endOfDayStars[0], delay));
                break;
            case StarRating.TWO:
                StartCoroutine(Activate(endOfDayStars[0], delay));
                StartCoroutine(Activate(endOfDayStars[0], delay + delayBetweenStars));
                break;
            case StarRating.THREE:
                StartCoroutine(Activate(endOfDayStars[0], delay));
                StartCoroutine(Activate(endOfDayStars[1], delay + delayBetweenStars));
                StartCoroutine(Activate(endOfDayStars[2], delay + delayBetweenStars * 2));
                break;
            default:
                break;
        }
    }

	public void ActivateFinalDayStars(StarRating rating, float delay) {
		for (int i = 0; i < endOfDayStars.Count; i++) {
			finalDayStars[i].ResetStar();
		}

		switch (rating) {
			case StarRating.ZERO:
				break;
			case StarRating.ONE:
                StartCoroutine(Activate(finalDayStars[0], delayBetweenStars));
                break;
			case StarRating.TWO:
                StartCoroutine(Activate(finalDayStars[0], delayBetweenStars));
                StartCoroutine(Activate(finalDayStars[1], delayBetweenStars));
				break;
			case StarRating.THREE:
                StartCoroutine(Activate(finalDayStars[0], delayBetweenStars));
                StartCoroutine(Activate(finalDayStars[1], delayBetweenStars));
				StartCoroutine(Activate(finalDayStars[2], delayBetweenStars * 2));
				break;
			default:
				break;
		}
	}

	private IEnumerator Activate (Star star, float delay) {
        yield return new WaitForSeconds(delay);
        star.ActivateStar(activationInfo);
    }
}
