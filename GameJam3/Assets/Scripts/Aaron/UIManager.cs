using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private float currentStatsDelay;

    [SerializeField]
    private float componentsSelectionDelay;

    private DropDownMenu currentStats;
    private DropDownMenu componentsSelection;
    private SlideInUI fireButton;
    private SlideInUI flyHUD;

    private RectTransform[] componentButtons;

    private Shop shop;

    private bool[] selectedCatagories;

    private void Awake()
    {
        selectedCatagories = new bool[3];

        shop = GetComponent<Shop>();

        fireButton = GameObject.FindGameObjectWithTag("FireButton").GetComponent<SlideInUI>();

        flyHUD = GameObject.FindGameObjectWithTag("FlyHUD").GetComponent<SlideInUI>();

        Transform ComponentSelection = GameObject.FindGameObjectWithTag("ComponentSelection").transform;

        currentStats = GameObject.FindGameObjectWithTag("CurrentStats").GetComponent<DropDownMenu>();
        componentsSelection = ComponentSelection.GetComponent<DropDownMenu>();

        componentButtons = new RectTransform[3];

        componentButtons[0] = ComponentSelection.GetChild(0).GetComponent<RectTransform>();
        componentButtons[1] = ComponentSelection.GetChild(1).GetComponent<RectTransform>();
        componentButtons[2] = ComponentSelection.GetChild(2).GetComponent<RectTransform>();

        Invoke("ShowCurrentStats", currentStatsDelay);
        Invoke("ShowComponentsSelection", componentsSelectionDelay);
    }

    private void ShowCurrentStats()
    {
        currentStats.TogglePullDown();
    }

    private void ShowComponentsSelection()
    {
        componentsSelection.TogglePullDown();
    }

    public void ToggleFlyHUD()
    {
        flyHUD.TogglePullDown();
    }

    public void ToggleUI()
    {
        currentStats.TogglePullDown();
        componentsSelection.TogglePullDown();
        fireButton.TogglePullDown();
    }

    public void SelectCatagory(int index)
    {
        shop.SelectCatagory(index);
        selectedCatagories[index] = true;

        for (int i = 0; i < 3; i++)
        {
            componentButtons[i].anchoredPosition = new Vector2(componentButtons[i].anchoredPosition.x, -85.0f);
        }

        for (int i = 0; i < 3; i++)
        {
            if (i != index)
            {
                componentButtons[i].anchoredPosition = new Vector2(componentButtons[i].anchoredPosition.x, componentButtons[i].anchoredPosition.y - 15);
            }
        }

        if (selectedCatagories[0] && selectedCatagories[1] && selectedCatagories[2])
        {
            if (!fireButton.PulledDown)
            {
                fireButton.TogglePullDown();
            }
        }
    }
}