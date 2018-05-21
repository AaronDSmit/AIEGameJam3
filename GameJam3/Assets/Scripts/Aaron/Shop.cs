using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Shop : MonoBehaviour
{
    private CustomerGenerator cusomter;

    private JetPack jetPack;

    [SerializeField]
    private Part[] fuelTanks;

    [SerializeField]
    private Part[] thruster;

    [SerializeField]
    private Part[] body;

    public void Launch()
    {
        jetPack.Weight = 10.0f;
        jetPack.VSpeed = 20.0f;
        jetPack.BurnTime = 1.0f;


        jetPack.TakeOff();
    }
}

[System.Serializable]
public struct Part
{
    public float weight;

    public float thrustPower;

    public float thrustDuration;
};