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

    public void ActivateEndOfDayStars (StarRating rating) {
        for (int i = 0; i < endOfDayStars.Count; i++) {
            endOfDayStars[i].ResetStar();
        }

        switch (rating) {
            case StarRating.ZERO:
                break;
            case StarRating.ONE:
                endOfDayStars[0].ActivateStar(activationInfo);
                break;
            case StarRating.TWO:
                endOfDayStars[0].ActivateStar(activationInfo);
                StartCoroutine(Activate(endOfDayStars[1], delayBetweenStars));
                break;
            case StarRating.THREE:
                endOfDayStars[0].ActivateStar(activationInfo);
                StartCoroutine(Activate(endOfDayStars[1], delayBetweenStars));
                StartCoroutine(Activate(endOfDayStars[2], delayBetweenStars * 2));
                break;
            default:
                break;
        }
    }

	public void ActivateFinalDayStars(StarRating rating) {
		for (int i = 0; i < endOfDayStars.Count; i++) {
			finalDayStars[i].ResetStar();
		}

		switch (rating) {
			case StarRating.ZERO:
				break;
			case StarRating.ONE:
				finalDayStars[0].ActivateStar(activationInfo);
				break;
			case StarRating.TWO:
				finalDayStars[0].ActivateStar(activationInfo);
				StartCoroutine(Activate(finalDayStars[1], delayBetweenStars));
				break;
			case StarRating.THREE:
				finalDayStars[0].ActivateStar(activationInfo);
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
