using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DayUI))]
[RequireComponent(typeof(DescriptionLibrary))]
public class Day : MonoBehaviour {
	[SerializeField] private int totalDays;
	[SerializeField] private int customersPerDay;

	public int CurrentDay { get { return currentDay; } }
	public int TodaysCustomers { get { return todaysCustomers; } }

	private int currentDay;
	private int todaysCustomers;
	private int aliveCustomers;
	private int totalAliveCustomers;
	private int deadCustomers;
	private int totalDeadCustomers;

	private DayUI dayUI;
	private DescriptionLibrary descriptionLibrary;
	private bool dayEnded;

	private void Start () {
		dayUI = GetComponent<DayUI>();
		descriptionLibrary = GetComponent<DescriptionLibrary>();
		StartDay();
	}

	private void Update() {
		if (dayEnded == false)
			return;

		if (currentDay < totalDays && Input.GetMouseButtonDown(0)) {
			StartDay();
		}
	}

	public void OrderComplete(bool customerSurvived) {
		dayUI.StartDay(currentDay);

		todaysCustomers++;

		if (customerSurvived)
			aliveCustomers++;
		else
			deadCustomers++;

		if (todaysCustomers > customersPerDay) {
			EndDay();
			return;
		}

		// Do spawn customer stuff here
	}

	private void StartDay() {
		currentDay++;
		dayUI.StartDay(currentDay);
		dayEnded = false;

		todaysCustomers = 1;
		aliveCustomers = 0;
		deadCustomers = 0;

		// Do spawn customer stuff here
	}

	private void EndDay() {
		Debug.Log("Note to Dan: Ask Aaron about the UI movement stuff");
		dayEnded = true;

		totalAliveCustomers += aliveCustomers;
		totalDeadCustomers += deadCustomers;

		if (currentDay >= totalDays) {
			EndGame();
			return;
		}

		dayUI.EndDay(currentDay, descriptionLibrary.GetDescriptionByRating(Rating.GREAT), 100, aliveCustomers, deadCustomers);
	}

	private void EndGame() {
		Debug.Log("Note to Dan: Work out this with team");
	}
}
