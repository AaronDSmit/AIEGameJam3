using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Description", menuName = "Description/Create New", order = 0)]
public class Description : ScriptableObject {
	[SerializeField] private StarRating rating;
	[SerializeField] private string descriptionText;

	public StarRating Rating { get { return rating; } }
	public string DescriptionText { get { return descriptionText; } }
}
