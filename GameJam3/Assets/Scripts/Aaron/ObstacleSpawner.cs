using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject obstaclePrefab;

    [SerializeField]
    private float maxHeight;

    [SerializeField]
    private float minHeight;

    [SerializeField]
    private float minDistanceBetween;

    [SerializeField]
    private float maxDistanceBetween;

    private List<GameObject> obstacles;

    public void ClearObstacles()
    {
        for (int i = 0; i < obstacles.Count; i++)
        {
            Destroy(obstacles[i]);
        }

        obstacles.Clear();
    }

    private void Awake()
    {
        obstacles = new List<GameObject>();
    }

    private void Start()
    {
        PopulateObstacles();
    }

    public void PopulateObstacles()
    {
        float currentY = minHeight;

        while (currentY < maxHeight)
        {
            float randomHeight = currentY + Random.Range(minDistanceBetween, maxDistanceBetween);

            GameObject newObstactle = Instantiate(obstaclePrefab, new Vector3(0.0f, randomHeight, 0.0f), Quaternion.identity);

            currentY = randomHeight;
            obstacles.Add(newObstactle);
        }
    }
}