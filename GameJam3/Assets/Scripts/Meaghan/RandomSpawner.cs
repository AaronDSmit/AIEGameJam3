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
    [Tooltip("How long you want spawning to go for.")]
    #endregion



    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Spawn()
    {
        Vector3 pos = centre + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2), Random.Range(-size.y / 2, size.y / 2));
    }
}
