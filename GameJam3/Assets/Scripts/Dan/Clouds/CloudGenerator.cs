using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudGenerator : MonoBehaviour {
	[SerializeField] private GameObject followObject;

	[Header("Spawn Area")]
	[SerializeField] private float minSpawnHeight;
    [SerializeField] private float maxSpawnHeight;
	[SerializeField] private float despawnHeight;
	[SerializeField] private float minXPosition;
	[SerializeField] private float maxXPosition;

	[Header("Spawn Frequencies")]
	[SerializeField] private float minSpawnTime;
	[SerializeField] private float maxSpawnTime;
	[SerializeField] private float minCloudNumber;
	[SerializeField] private float maxCloudNumber;

	[Header("Cloud Objects")]
	[SerializeField] private List<GameObject> objects;
	[SerializeField] private List<float> depths;
	[SerializeField] private float minCloudSpeed;
	[SerializeField] private float maxCloudSpeed;

    [SerializeField] private bool forceStartProduction;

    private bool producingClouds;
	private float currentSpawnTime;
	private float spawnCount;

	private int currentCloudNumber;
	private Transform cloudParent;

	private void Start() {
		ResetSpawnTime();

		cloudParent = new GameObject().transform;
		cloudParent.name = "Cloud Parent";
	}

	public void ToggleProduction(bool producing) {
		producingClouds = producing;
	}

	private void Update() {
		UpdatePosition();
		UpdateCloudSpawn();

        if (forceStartProduction) {
            forceStartProduction = false;
            ToggleProduction(true);
        }
	}

	public void CloudDespawned() {
		currentCloudNumber--;
	}

	private void UpdatePosition() {
		if (followObject == null)
			return;

		transform.position = followObject.transform.position;
	}

	private void UpdateCloudSpawn() {
		if (currentCloudNumber >= maxCloudNumber)
			return;

		if (currentCloudNumber < minCloudSpeed) {
			SpawnCloud();
			return;
		}

		spawnCount += Time.deltaTime;

		if(spawnCount >= currentSpawnTime) {
			ResetSpawnTime();
			SpawnCloud();
		}
	}

	private void SpawnCloud() {
		currentCloudNumber++;

		GameObject cloud = Instantiate(objects[Random.Range(0, objects.Count)]);
		cloud.transform.SetParent(cloudParent);

		float xPosition = Random.Range(0, 2) == 0 ? Random.Range(minXPosition, 0) : Random.Range(0, maxXPosition);
		float zPosition = depths[Random.Range(0, depths.Count)];

		Vector3 position = new Vector3(xPosition,
			transform.position.y + Random.Range(minSpawnHeight, maxSpawnHeight),
			zPosition);

        cloud.transform.position = position;

        if (cloud.GetComponent<Cloud>()) {
            cloud.GetComponent<Cloud>().Init(this, xPosition < 0 ? Vector3.right : Vector3.left, zPosition,
            Random.Range(minCloudSpeed, maxCloudSpeed),
            xPosition == minXPosition ? maxXPosition : minXPosition, despawnHeight);
        }
	}

	private void ResetSpawnTime() {
		spawnCount = 0f;
		currentSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);
	}

	private void OnDrawGizmosSelected() {
		Gizmos.color = Color.green;

		Gizmos.DrawWireCube(new Vector3(transform.position.x, transform.position.y + minSpawnHeight, transform.position.z), 
			new Vector3(Mathf.Abs(minXPosition) + maxXPosition, 1, 1));

		Gizmos.DrawWireCube(new Vector3(transform.position.x, transform.position.y + despawnHeight, transform.position.z),
			new Vector3(Mathf.Abs(minXPosition) + maxXPosition, 1, 1));
	}
}
