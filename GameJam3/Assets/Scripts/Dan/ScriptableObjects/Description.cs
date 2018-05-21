using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Description", menuName = "Description/Create New", order = 0)]
public class Description : ScriptableObject {
	[SerializeField] private Rating rating;
	[SerializeField] private string descriptionText;

	public Rating Rating { get { return rating; } }
	public string DescriptionText { get { return descriptionText; } }
}
