using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    private CustomerGenerator customerGen;
    private UIManager UI;

    private SecondJetpack jetPack;

    [SerializeField]
    private float launchDelay;

    [SerializeField]
    private Part[] fuelTanks;

    [SerializeField]
    private Part[] thruster;

    [SerializeField]
    private Part[] body;

    private int[] currentComponent;
    private int selectedComponent;

    private void Awake()
    {
        jetPack = FindObjectOfType<SecondJetpack>();
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
        }
    }

    public void Launch()
    {
        jetPack.Weight = 10.0f;
        jetPack.VSpeed = 20.0f;
        jetPack.BurnTime = 1.0f;

        UI.ToggleUI();

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
    public float weight;

    public float thrustPower;

    public float thrustDuration;
};