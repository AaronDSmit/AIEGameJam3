using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StarRating { ZERO, ONE, TWO, THREE }

public class DescriptionLibrary : MonoBehaviour {
	[SerializeField] private List<Description> descriptions;

	public string GetDescriptionByRating(StarRating rating) {
        if (descriptions.Count <= 0)
            return string.Empty;

		List<Description> descriptionRating = descriptions.FindAll(d => d.Rating == rating);

		if (descriptionRating.Count <= 0)
			return string.Empty;

		return descriptionRating[Random.Range(0, descriptionRating.Count)].DescriptionText;
	}
}
