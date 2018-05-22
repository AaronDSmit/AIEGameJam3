using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DayUI))]
[RequireComponent(typeof(DescriptionLibrary))]
[RequireComponent(typeof(RatingCalculator))]
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

    private UIManager uiManager;
	private DayUI dayUI;
    private StarRatingUI starRatingUI;
    private RatingCalculator ratingCalculator;
	private DescriptionLibrary descriptionLibrary;
	private bool dayEnded;

	private void Start () {
		dayUI = GetComponent<DayUI>();
        uiManager = FindObjectOfType<UIManager>();
        ratingCalculator = GetComponent<RatingCalculator>();
        starRatingUI = GetComponent<StarRatingUI>();
        descriptionLibrary = GetComponent<DescriptionLibrary>();
		StartDay(false);
	}

	private void Update() {
		if (dayEnded == false)
			return;

		if (currentDay < totalDays && Input.GetMouseButtonDown(0))
			StartDay();
	}

	public void OrderComplete(bool customerSurvived) {
		dayUI.StartDay(currentDay);

		todaysCustomers++;

		if (customerSurvived)
			aliveCustomers++;
		else
			deadCustomers++;

		if (todaysCustomers > customersPerDay)
			EndDay();
	}

	private void StartDay(bool toggleUI = true) {
		currentDay++;
		dayUI.StartDay(currentDay);
		dayEnded = false;

        if (toggleUI)
            uiManager.ToggleUI();

		todaysCustomers = 1;
		aliveCustomers = 0;
		deadCustomers = 0;
	}

	private void EndDay() {
		dayEnded = true;
        uiManager.ToggleUI();

        totalAliveCustomers += aliveCustomers;
		totalDeadCustomers += deadCustomers;

		if (currentDay >= totalDays) {
			EndGame();
			return;
		}

        StarRating rating = ratingCalculator.GetStarRating(aliveCustomers, customersPerDay);

		dayUI.EndDay(currentDay, descriptionLibrary.GetDescriptionByRating(rating), 100, aliveCustomers, deadCustomers);
        starRatingUI.ActivateStars(rating);

    }

	private void EndGame() {
		Debug.Log("Note to Dan: Work out this with team");
	}
}
