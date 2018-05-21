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
    private float lerpTime = 1.0f;
    private float currentLerpTime = 0.0f;
    private float targetHeight;
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

    void Update()
    {
        if (launch)
        {
            //Increase timer
            timer += Time.deltaTime;

            //Increment timer once per frame
            currentLerpTime += Time.deltaTime;
            if (currentLerpTime > lerpTime)
            {
                currentLerpTime = lerpTime;
            }

            if (timer < burnTime)
            {
                //Begin to lerp
                float t = currentLerpTime / lerpTime;
                t = 1.0f - Mathf.Cos(t * Mathf.PI * 0.5f);


                //Move up
                transform.position = new Vector3(transform.position.x, Mathf.Lerp(0, targetHeight, t), transform.position.z);
            }
        }
    }


    public void TakeOff()
    {
        //Calculate target height
        targetHeight = (vSpeed - weight) * burnTime;

        //Launch
        launch = true;
    }
}
