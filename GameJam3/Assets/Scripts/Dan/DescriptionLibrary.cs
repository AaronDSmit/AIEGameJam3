using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StarRating { ZERO, ONE, TWO, THREE }

public class DescriptionLibrary : MonoBehaviour {
	[SerializeField] private List<Description> endOfDayDescriptions;
	[SerializeField] private List<Description> finalDayDescriptions;

	public string GetEndOfDayDescription(StarRating rating) {
        if (endOfDayDescriptions.Count <= 0)
            return string.Empty;

		List<Description> descriptionRating = endOfDayDescriptions.FindAll(d => d.Rating == rating);

		if (descriptionRating.Count <= 0)
			return string.Empty;

		return descriptionRating[Random.Range(0, descriptionRating.Count)].DescriptionText;
	}

	public string GetFinalDayDescription(StarRating rating) {
		if (endOfDayDescriptions.Count <= 0)
			return string.Empty;

		List<Description> descriptionRating = finalDayDescriptions.FindAll(d => d.Rating == rating);

		if (descriptionRating.Count <= 0)
			return string.Empty;

		return descriptionRating[Random.Range(0, descriptionRating.Count)].DescriptionText;
	}
}
