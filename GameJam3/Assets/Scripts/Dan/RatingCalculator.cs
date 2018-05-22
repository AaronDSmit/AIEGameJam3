using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatingCalculator : MonoBehaviour {
    [SerializeField, Range(0.1f, 1)] private float ratingThreshold; 

    public StarRating GetStarRating (int livingCustomers, int totalCustomers) {
        if(livingCustomers == 0)
            return StarRating.ZERO;

        if (livingCustomers == totalCustomers)
            return StarRating.THREE;

        float perc = livingCustomers / totalCustomers;

        if (perc <= ratingThreshold)
            return StarRating.ONE;
        else
            return StarRating.TWO;
    }
}