using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    private CustomerGenerator customerGen;
    private UIManager UI;
    private Customer currentCustomer;

    private JetPack jetPack;

    [SerializeField]
    private float launchDelay;

    [SerializeField]
    private Part[] Customers;

    [SerializeField]
    private Part[] Jetpack;

    [SerializeField]
    private Part[] Thruster;

    [SerializeField]
    private Part[] gemType;

    private bool flying;

    private int currentCustomerIndex;

    private float maxTurnPower = 400.0f;
    private float maxBurnTime = 400.0f;

    private float currentTurnPower;
    private float currentBurnTime;

    private GameObject fuelTypes;

    private int[] currentComponent;
    private int selectedComponent;

    private void Awake()
    {
        jetPack = FindObjectOfType<JetPack>();
        customerGen = GetComponent<CustomerGenerator>();

        UI = GetComponent<UIManager>();

        selectedComponent = -1;

        currentComponent = new int[3];

        currentCustomer = FindObjectOfType<Customer>();

        flying = false;
    }

    private void Start()
    {
        UI.SelectCatagory(0);
        UpdateStatsUI();
    }

    private void Update()
    {
        if (!flying)
        {
            if (MobileInput.SwipedRight)
            {
                ChangeSelectedComponent(+1);
            }

            if (MobileInput.SwipedLeft)
            {
                ChangeSelectedComponent(-1);
            }
        }
    }

    public bool Flying
    {
        set { flying = value; }
    }

    private void ChangeSelectedComponent(int change)
    {
        currentComponent[selectedComponent] += change;

        if (currentComponent[selectedComponent] < 0)
        {
            currentComponent[selectedComponent] = 2;
        }
        else if (currentComponent[selectedComponent] > 2)
        {
            currentComponent[selectedComponent] = 0;
        }

        // Update visuals
        if (selectedComponent == 0) // Jetpack
        {
            currentCustomer.SetWings(Jetpack[currentComponent[selectedComponent]].sprite);
        }
        else if (selectedComponent == 1) // Thruster
        {
            currentCustomer.SetThruster(Thruster[currentComponent[selectedComponent]].sprite);
        }
        else if (selectedComponent == 2) // gem
        {
            currentCustomer.SetGem(gemType[currentComponent[selectedComponent]].sprite);
        }

        UpdateStatsUI();
    }

    private void UpdateStatsUI()
    {
        CalculateCurrentStats();
        UI.UpdateStatBars(currentTurnPower / maxTurnPower, currentBurnTime / maxBurnTime);
    }

    private void CalculateCurrentStats()
    {
        currentTurnPower = 0.0f;
        currentBurnTime = 0.0f;

        currentBurnTime += Jetpack[currentComponent[0]].burnTime;
        currentTurnPower += Jetpack[currentComponent[0]].turnSpeed;

        currentBurnTime += Thruster[currentComponent[1]].burnTime;
        currentTurnPower += Thruster[currentComponent[1]].turnSpeed;

        currentBurnTime += gemType[currentComponent[2]].burnTime;
        currentTurnPower += gemType[currentComponent[2]].turnSpeed;

        currentBurnTime += Customers[currentCustomerIndex].burnTime;
        currentTurnPower += Customers[currentCustomerIndex].turnSpeed;
    }

    public void Launch()
    {
        flying = true;

        jetPack.BurnTime = 50.0f;
        jetPack.TurningAngle = 5.0f;

        UI.ToggleUI();

        ScreenShake.instance.ShakeEaseOut(60);

        Invoke("FireJetpack", launchDelay);
    }

    private void FireJetpack()
    {
        jetPack.TakeOff();
        UI.ToggleFlyHUD();
    }

    public void SelectCatagory(int index)
    {
        selectedComponent = index;
    }

    public void ArrivedSafely()
    {
        UI.FadeOutIn();
    }
}

[System.Serializable]
public struct Part
{
    public float turnSpeed;

    public float burnTime;

    public Sprite sprite;
};