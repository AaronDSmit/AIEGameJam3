using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private float currentStatsDelay;

    [SerializeField]
    private float componentsSelectionDelay;

    private Image fadePlane = null;

    private JetPack jetPack;
    private JetpackCamera jCamera;

    private DropDownMenu currentStats;
    private DropDownMenu componentsSelection;
    private SlideInUI fireButton;
    private SlideInUI flyHUD;

    private BarAnimation burnTimeBar;
    private BarAnimation turningPowerBar;

    private RectTransform[] componentButtons;

    private Shop shop;

    private bool[] selectedCatagories;

    private void Awake()
    {
        fadePlane = GameObject.FindGameObjectWithTag("FadeScreen").GetComponent<Image>();

        BarAnimation[] statBars = FindObjectsOfType<BarAnimation>();

        burnTimeBar = statBars[0];
        turningPowerBar = statBars[1];

        jetPack = FindObjectOfType<JetPack>();
        jCamera = FindObjectOfType<JetpackCamera>();

        fadePlane.gameObject.SetActive(true);

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

        if (fireButton.PulledDown)
        {
            fireButton.TogglePullDown();
        }
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

    public void UpdateStatBars(float burnTimePercent, float turningPowerPercent)
    {
        burnTimeBar.UpdateValue(burnTimePercent);
        turningPowerBar.UpdateValue(turningPowerPercent);
    }

    public void FadeOutIn()
    {
        StartCoroutine(FadeYoYo());
    }

    private IEnumerator FadeYoYo()
    {
        StartCoroutine(FadeImage(0, 1, 1.0f));

        yield return new WaitForSeconds(1.1f);

        // move camera
        ToggleFlyHUD();
        jetPack.ResetJetpack();
        jCamera.ResetCamera();
        ScreenShake.instance.StopScreenShake();


        selectedCatagories[0] = false;
        selectedCatagories[2] = false;
        selectedCatagories[2] = false;

        SelectCatagory(0);

        yield return new WaitForSeconds(0.5f);

        StartCoroutine(FadeImage(1, 0, 1.0f));
        ToggleUI();
    }

    private IEnumerator FadeImage(float from, float to, float time)
    {
        float currentLerpTime = 0;
        float t = 0;

        while (t < 1)
        {
            currentLerpTime += Time.deltaTime;
            t = currentLerpTime / time;

            fadePlane.color = new Color(fadePlane.color.r, fadePlane.color.g, fadePlane.color.b, Mathf.Lerp(from, to, t));

            yield return null;
        }
    }
}