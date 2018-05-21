using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelHUD : MonoBehaviour
{
    [SerializeField]
    private float maxHeight = 1000.0f;

    private RectTransform fuelIndicator;

    private void Awake()
    {
        fuelIndicator = GetComponent<RectTransform>();
    }

    private void Start()
    {
        UpdateFuel(1.0f);
    }

    public void UpdateFuel(float percent)
    {
        fuelIndicator.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, percent * maxHeight);
    }
}