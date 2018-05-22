using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    #region Inspector Variables
    [Tooltip("The centre of the spawn area.")]
    [SerializeField]
    private Vector3 centre;
    [Tooltip("The size of the spawn area.")]
    [SerializeField]
    private Vector3 size;
    [SerializeField]
    private GameObject spawnObject;
    [Tooltip("How long the spawn goes for.")]
    [SerializeField]
    private float spawnTime = 1.0f;
    #endregion

    private float timer;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        timer += Time.deltaTime;

        if(timer < spawnTime)
        {
            Spawn();
        }
	}

    void Spawn()
    {
        Vector3 pos = centre + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2), Random.Range(-size.y / 2, size.y / 2));

        Instantiate(spawnObject, pos, Quaternion.identity);
    }
}
