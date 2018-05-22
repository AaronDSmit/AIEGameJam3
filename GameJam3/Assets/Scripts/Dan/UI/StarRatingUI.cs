using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarRatingUI : MonoBehaviour {
    [SerializeField] private RectTransform starParent;
    [SerializeField] private StarActivation activationInfo;
    [SerializeField] private float delayBetweenStars;

    private List<Star> stars;

    private void Start () {
        stars = new List<Star>();

        foreach (Transform child in starParent.transform) {
            stars.Add(child.GetComponent<Star>());
        }
    }

    public void ActivateStars (StarRating rating) {
        for (int i = 0; i < stars.Count; i++) {
            stars[i].ResetStar();
        }

        switch (rating) {
            case StarRating.ZERO:
                break;
            case StarRating.ONE:
                stars[0].ActivateStar(activationInfo);
                break;
            case StarRating.TWO:
                stars[0].ActivateStar(activationInfo);
                StartCoroutine(Activate(stars[1], delayBetweenStars));
                break;
            case StarRating.THREE:
                stars[0].ActivateStar(activationInfo);
                StartCoroutine(Activate(stars[1], delayBetweenStars));
                StartCoroutine(Activate(stars[2], delayBetweenStars * 2));
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
