using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Rating { TERRIBLE, POOR, AVERAGE, GOOD, GREAT }

public class DescriptionLibrary : MonoBehaviour {
	[SerializeField] private List<Description> descriptions;

	public string GetDescriptionByRating(Rating rating) {
		List<Description> descriptionRating = descriptions.FindAll(d => d.Rating == rating);

		if (descriptionRating == null)
			return string.Empty;

		return descriptionRating[Random.Range(0, descriptionRating.Count)].DescriptionText;
	}
}
