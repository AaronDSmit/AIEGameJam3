using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerGenerator : MonoBehaviour
{
    private Order currentOrder;

    public void GenerateOrder()
    {
        currentOrder.minHeight = Random.Range(100, 250);
        currentOrder.maxHeight = Random.Range(250, 300);

        Debug.Log("Hi! I want a to travel " + currentOrder.minHeight + " to " + currentOrder.maxHeight);
    }
}

[System.Serializable]
public struct Order
{
    public float minHeight;
    public float maxHeight;
};