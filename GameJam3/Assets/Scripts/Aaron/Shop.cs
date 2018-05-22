using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    private CustomerGenerator customerGen;
    private UIManager UI;

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
    }

    private void Update()
    {
        if (MobileInput.SwipedRight)
        {
            Debug.Log("Swiped Right");

            ChangeSelectedComponent(+1);
        }

        if (MobileInput.SwipedLeft)
        {
            Debug.Log("Swiped Left");

            ChangeSelectedComponent(-1);
        }
    }

    private void ChangeSelectedComponent(int change)
    {
        if (selectedComponent > 0 && selectedComponent < 3)
        {
            currentComponent[selectedComponent] += change;

            if (selectedComponent == 0) // Customer
            {

            }
        }
    }

    public void Launch()
    {
        jetPack.BurnTime = 10.0f;
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

    }
}


[System.Serializable]
public struct Part
{
    public float turnSpeed;

    public float burnTime;

    public Sprite sprite;
};