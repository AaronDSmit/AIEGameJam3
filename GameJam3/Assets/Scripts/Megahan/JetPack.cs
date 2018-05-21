using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetPack : MonoBehaviour
{

    #region Public Variables
    [Tooltip("Time it takes to lerp to destination.")]
    [SerializeField]
    private float lerpTime = 1.0f;
    #endregion

    #region Variables
    public float vSpeed = 0.0f;
    public float burnTime = 0.0f;
    public float weight = 0.0f;
    private bool launch = false;
    private float currentLerpTime = 0.0f;
    private float targetHeight;
    private float t = 0.0f;
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

        if (launch && t < 1.0f)
        {
            //Increment timer once per frame
            currentLerpTime += Time.deltaTime;

            //Begin to lerp
            t = currentLerpTime / lerpTime;
            t = 1.0f - Mathf.Cos(t * Mathf.PI * 0.5f);

            //Move up
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(0, targetHeight, t), transform.position.z);
        }
    }

    public void TakeOff()
    {
        //Calculate target height
        targetHeight = (vSpeed - weight) * burnTime;

        //Launch
        launch = true;

        t = 0;
        currentLerpTime = 0.0f;

    }
}
