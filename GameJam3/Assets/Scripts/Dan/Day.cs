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

    public bool EndOfDay { get { return todaysCustomers >= customersPerDay; } }
    public bool EndOfWeek { get { return EndOfDay && currentDay >= totalDays; } }

	private int currentDay;
	private int todaysCustomers;
	private int aliveCustomers;
	private int totalAliveCustomers;
	private int deadCustomers;
	private int totalDeadCustomers;

	private DayUI dayUI;
    private StarRatingUI starRatingUI;
    private RatingCalculator ratingCalculator;
	private DescriptionLibrary descriptionLibrary;
	private bool dayEnded;

	private void Start () {
		dayUI = GetComponent<DayUI>();
        ratingCalculator = GetComponent<RatingCalculator>();
        starRatingUI = GetComponent<StarRatingUI>();
        descriptionLibrary = GetComponent<DescriptionLibrary>();

        StartDay();
	}

	private void Update() {
		if (dayEnded == false)
			return;

		if (currentDay < totalDays && Input.GetMouseButtonDown(0))
			StartDay();
	}

	public void OrderComplete(bool customerSurvived) {
		todaysCustomers++;

		if (customerSurvived)
			aliveCustomers++;
		else
			deadCustomers++;

        StartDay();
	}

	private void StartDay() {
		currentDay++;
		dayUI.StartDay(currentDay);
		dayEnded = false;
		todaysCustomers = 1;
		aliveCustomers = 0;
		deadCustomers = 0;
	}

	public void EndDay(float delayTime) {
		dayEnded = true;

        totalAliveCustomers += aliveCustomers;
		totalDeadCustomers += deadCustomers;

		if (currentDay >= totalDays) {
			EndGame();
			return;
		}

        StarRating rating = ratingCalculator.GetStarRating(aliveCustomers, customersPerDay);

		dayUI.EndDay(currentDay, descriptionLibrary.GetEndOfDayDescription(rating), 100, aliveCustomers, deadCustomers);
		StartCoroutine(ActivateStarRating(rating));
    }

	private void EndGame() {
		StarRating rating = ratingCalculator.GetStarRating(totalAliveCustomers, totalAliveCustomers + totalDeadCustomers);

		dayUI.FinalDay(currentDay, descriptionLibrary.GetFinalDayDescription(rating), totalAliveCustomers, totalDeadCustomers);
		StartCoroutine(ActivateStarRating(rating, true));
	}

	private IEnumerator ActivateStarRating(StarRating rating, bool finalDay = false) {
		yield return new WaitForEndOfFrame();

		if (finalDay)
			starRatingUI.ActivateFinalDayStars(rating);
		else
			starRatingUI.ActivateEndOfDayStars(rating);
	}
}
