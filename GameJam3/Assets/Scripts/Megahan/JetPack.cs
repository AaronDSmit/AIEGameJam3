using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetPack : MonoBehaviour
{
    #region Variables
    private float vSpeed = 0.0f;
    private float burnTime = 0.0f;
    private float weight = 0.0f;
    private float timer = 0.0f;
    private bool launch = false;
    private float flightSpeed = 0.0f;
    #endregion

    #region Getters and Setters
    public float VSpeed
    {
        get { return vSpeed; }
        set { vSpeed = value; }
    }

    public float Weight
    {
        get { return weight; }
        set { weight = value; }
    }

    public float BurnTime
    {
        get { return burnTime; }
        set { burnTime = value; }
    }


    #endregion


    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(launch)
        {
            if (timer < burnTime)
            {
                //Calulate flightSpeed
                flightSpeed = vSpeed - weight * Time.deltaTime;

                //Move up
                transform.position += flightSpeed * Vector3.up;
            }
        }
	}


    void TakeOff()
    {
        launch = true;
    }
}
