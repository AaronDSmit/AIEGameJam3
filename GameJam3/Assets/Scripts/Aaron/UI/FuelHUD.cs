using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelHUD : MonoBehaviour
{
    [SerializeField]
    private float maxHeight = 1000.0f;

    private RectTransform fuelIndicator;

    private Text heightText;

    private JetPack jetpack;

    private void Awake()
    {
        fuelIndicator = transform.GetChild(1).GetChild(0).GetComponent<RectTransform>();

        heightText = GetComponentInChildren<Text>();

        jetpack = FindObjectOfType<JetPack>();
    }

    private void Update()
    {
        heightText.text = (int)jetpack.transform.position.y + "M";
        fuelIndicator.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, jetpack.BurnTimePercent * maxHeight);
    }
}