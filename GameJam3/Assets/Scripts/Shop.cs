using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Shop : MonoBehaviour
{
    [SerializeField]
    private Part[] fuelTanks;

    [SerializeField]
    private Part[] thruster;

    [SerializeField]
    private Part[] body;
}

[System.Serializable]
public struct Part
{
    public float weight;

    public float thrustPower;

    public float thrustDuration;
};