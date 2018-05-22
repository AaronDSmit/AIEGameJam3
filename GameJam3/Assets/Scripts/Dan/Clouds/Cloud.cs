using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour {
	private bool isInitialised;
	private CloudGenerator cloudGenerator;
	private Vector3 direction;
	private float depth;
	private float speed;
	private float despawnXPosition;

	public void Init(CloudGenerator cloudGenerator, Vector3 direction, float depth, float speed, float despawnXPosition) {
		this.cloudGenerator = cloudGenerator;
		this.direction = direction;
		this.depth = depth;
		this.speed = speed;
		this.despawnXPosition = despawnXPosition;

		isInitialised = true;
	}

	private void Update() {
		UpdateMovement();
	}

	private void UpdateMovement() {
		if (isInitialised == false)
			return;

		Vector3 newLocation = transform.position + (direction * speed * Time.deltaTime);
		newLocation.z = depth;

		transform.position = newLocation;

		if(despawnXPosition < 0 && transform.position.x <= despawnXPosition ||
			despawnXPosition > 0 && transform.position.x >= despawnXPosition) {
			cloudGenerator.CloudDespawned();
			Destroy(gameObject);
		}
	}
}
