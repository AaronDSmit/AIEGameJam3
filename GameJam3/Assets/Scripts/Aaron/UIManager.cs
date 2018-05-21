using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    RectTransform fireButton;

    RectTransform[] componentButtons;

    private void Awake()
    {
        fireButton = GameObject.FindGameObjectWithTag("FireButton").GetComponent<RectTransform>();

        Transform ComponentSelection = GameObject.FindGameObjectWithTag("ComponentSelection").transform;

        componentButtons = new RectTransform[3];

        componentButtons[0] = ComponentSelection.GetChild(0).GetComponent<RectTransform>();
        componentButtons[1] = ComponentSelection.GetChild(1).GetComponent<RectTransform>();
        componentButtons[2] = ComponentSelection.GetChild(2).GetComponent<RectTransform>();
    }

    public void SelectCatagory(int index)
    {
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
    }
}