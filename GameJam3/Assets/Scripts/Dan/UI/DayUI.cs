﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayUI : MonoBehaviour {
	[Header("End of Day")]
	[SerializeField] private GameObject endOfDayUI;
	[SerializeField] private GameObject dayAlert;
	[SerializeField] private Text dayNumber;
	[SerializeField] private Text description;
	[SerializeField] private Text goldAmount;
	[SerializeField] private Text results;

	[Header("Final Day")]
	[SerializeField] private GameObject finalDayUI;
	[SerializeField] private Text finalDayNumber;
	[SerializeField] private Text finalDayDescription;
	//[SerializeField] private Text finalDayGoldAmount;
	[SerializeField] private Text finalDayResults;

	private Text dayAlertText;

	public void EndDay(int day, string newDescription, int gold, int alive, int dead) {
		endOfDayUI.SetActive(true);
		dayNumber.text = string.Format("Day {0}", day.ToString("D2"));
		description.text = newDescription;
		goldAmount.text = string.Format("{0}G", gold);
		results.text = string.Format("{0} alive {1} dead", alive, dead);

		dayAlert.SetActive(false);
	}

	public void StartDay(int day) {
		endOfDayUI.SetActive(false);
		dayAlert.SetActive(true);

		if (dayAlertText == null)
			dayAlertText = dayAlert.GetComponentInChildren<Text>();

		dayAlertText.text = string.Format("Day {0}", day.ToString("D2"));
	}

	public void FinalDay(int day, string newDescription, int alive, int dead) {
		finalDayUI.SetActive(true);
		finalDayNumber.text = string.Format("Day {0}", day.ToString("D2"));
		finalDayDescription.text = newDescription;
		//finalDayGoldAmount.text = string.Format("{0}G", gold);
		finalDayResults.text = string.Format("{0} alive {1} dead", alive, dead);
	}
}
