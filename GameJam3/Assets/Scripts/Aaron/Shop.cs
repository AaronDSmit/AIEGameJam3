using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    private CustomerGenerator customerGen;
    private UIManager UI;
    private Customer currentCustomer;
    private Day day;

    private JetPack jetPack;

    [SerializeField]
    private float longestBurnTime = 90.0f;

    [SerializeField]
    private float largestTurnPower = 7.0f;

    [SerializeField]
    private float maxDestinationHeight;

    [SerializeField]
    private float minDestinationHeight;

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

    [SerializeField]
    private GameObject destinationPrefab;

    private GameObject destinationGO;

    private float destination;
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

        day = GameObject.FindGameObjectWithTag("DayManager").GetComponent<Day>();

        UI = GetComponent<UIManager>();

        selectedComponent = -1;

        currentComponent = new int[3];

        currentCustomer = FindObjectOfType<Customer>();

        flying = false;
    }

    private void Start()
    {
        UI.SelectCatagory(0);
        GenerateRandomCustomer();
        UpdateStatsUI();
    }

    private void GenerateRandomCustomer()
    {
        currentCustomerIndex = Random.Range(0, Customers.Length);
        currentCustomer.SetCustomer(Customers[currentCustomerIndex].sprite);

        destination = Random.Range(minDestinationHeight, maxDestinationHeight);
        UI.ToggleDestinationUI(destination);
        SpawnDestination();
    }

    private void SpawnDestination()
    {
        if (destinationGO != null)
        {
            Destroy(destinationGO);
        }

        destinationGO = Instantiate(destinationPrefab, new Vector3(0, destination, 0), Quaternion.identity);
    }

    private void Update()
    {
        if (!flying)
        {
            if (Input.mousePosition.x < Screen.width / 2)
            {
                ChangeSelectedComponent(-1);
            }
            else
            {
                ChangeSelectedComponent(+1);
            }

            //if (MobileInput.SwipedRight)
            //{
            //    ChangeSelectedComponent(+1);
            //}

            //if (MobileInput.SwipedLeft)
            //{
            //    ChangeSelectedComponent(-1);
            //}
        }
    }

    public bool Flying
    {
        set
        {
            flying = value;

            if (!flying)
            {
                GenerateRandomCustomer();
            }
        }

        get { return flying; }
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

        UI.UpdateStatBars(currentBurnTime / maxBurnTime, currentTurnPower / maxTurnPower);
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
        if (!flying)
        {
            flying = true;

            jetPack.BurnTime = (currentBurnTime / maxBurnTime) * longestBurnTime;
            jetPack.TurningAngle = (currentTurnPower / maxTurnPower) * largestTurnPower;

            UI.ToggleUI();

            ScreenShake.instance.ShakeEaseOut(60);

            UI.FadeInScorchMark(launchDelay * 2.0f);

            Invoke("FireJetpack", launchDelay);
        }
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
        day.OrderComplete(true);

        UI.ShowResultsUI();

        StartCoroutine(CheckForTap());
    }

    private IEnumerator CheckForTap()
    {
        while (true)
        {
            if (MobileInput.Tap)
            {
                UI.FadeOutIn();
                break;
            }

            yield return null;
        }
    }
}

[System.Serializable]
public struct Part
{
    public float turnSpeed;

    public float burnTime;

    public Sprite sprite;
};