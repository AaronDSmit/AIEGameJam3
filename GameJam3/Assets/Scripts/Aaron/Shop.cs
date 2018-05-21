using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    private CustomerGenerator customerGen;

    private JetPack jetPack;

    [SerializeField]
    private Part[] fuelTanks;

    [SerializeField]
    private Part[] thruster;

    [SerializeField]
    private Part[] body;

    private void Awake()
    {
        jetPack = FindObjectOfType<JetPack>();
        customerGen = GetComponent<CustomerGenerator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            customerGen.GenerateOrder();
            Launch();
        }
    }

    public void Launch()
    {
        jetPack.Weight = 10.0f;
        jetPack.VSpeed = 20.0f;
        jetPack.BurnTime = 1.0f;


        jetPack.TakeOff();
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