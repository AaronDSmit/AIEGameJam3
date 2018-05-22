﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetPack : MonoBehaviour
{
    #region Inspector Variables
    [Tooltip("Time it takes to lerp to destination.")]
    [SerializeField]
    private float timeToMaxSpeed;

    [SerializeField]
    private float maxSpeed;

    [SerializeField]
    private float disableThrustDelay;

    [SerializeField]
    private float minX;

    [SerializeField]
    private float maxX;

    [SerializeField]
    private float offScreenKillDelay;

    [Tooltip("The vitcory range.")]
    [SerializeField]
    private float range;
    [Tooltip("The amount of fuel that gets reduced when you collide with an object.")]
    [SerializeField]
    private float fuelReduction;

    [SerializeField]
    private float minHeightControl;
    #endregion

    #region Variables
    private float vSpeed = 0.0f;
    private float burnTime = 0.0f;
    private float currentLerpTime = 0.0f;
    private float destination;
    private Shop shop;
    private bool belowTarget;
    private bool aboveTarget;
    private JetpackCamera Jcamera;
    private bool flying = false;
    private bool isFalling = false;
    private float turningAngle;
    private bool hasBurntFuel = false;
    private float fuelPercent = 1.0f;
    private float totalBurnTime;
    private bool dyingOffScreenCheck;

    private Vector3 startPos;
    private Quaternion startRot;

    private UIManager UI;
    #endregion

    #region Getters and Setters
    public float VSpeed
    {
        get { return vSpeed; }
        set { vSpeed = value; }
    }

    public float BurnTime
    {
        get { return burnTime; }
        set { burnTime = value; }
    }

    public float BurnTimePercent
    {
        get { return fuelPercent; }
    }


    public float Destination
    {
        get { return destination; }
        set { destination = value; }
    }

    public float TurningAngle
    {
        get { return turningAngle; }
        set { turningAngle = value; }
    }
    #endregion


    private void Awake()
    {
        //Get the shop
        shop = FindObjectOfType<Shop>();

        Jcamera = FindObjectOfType<JetpackCamera>();
        UI = FindObjectOfType<UIManager>();

        startPos = transform.position;
        startRot = transform.rotation;
    }

    private void DieOffScreen()
    {
        if (transform.position.x < minX || transform.position.x > maxX)
        {
            // Die
            UI.FadeOutIn();
        }
        else
        {
            dyingOffScreenCheck = false;
        }
    }

    public void ResetJetpack()
    {
        transform.position = startPos;
        transform.rotation = startRot;
        flying = false;
    }

    private void Update()
    {
        if (flying)
        {
            // check if off screen
            if (transform.position.x < minX || transform.position.x > maxX)
            {
                if (!dyingOffScreenCheck)
                {
                    dyingOffScreenCheck = true;

                    Invoke("DieOffScreen", offScreenKillDelay);
                }
            }

            if (transform.position.y > minHeightControl && Input.GetMouseButtonDown(0))
            {
                if (!isFalling)
                {
                    if (Input.mousePosition.x < Screen.width / 2)
                    {
                        // move left
                        transform.Rotate(Vector3.forward, turningAngle);
                    }
                    else
                    {
                        // move right
                        transform.Rotate(Vector3.forward, -turningAngle);
                    }
                }
                else
                {
                    if (Input.mousePosition.x < Screen.width / 2)
                    {
                        // move left
                        transform.Rotate(Vector3.forward, -turningAngle);
                    }
                    else
                    {
                        // move right
                        transform.Rotate(Vector3.forward, turningAngle);
                    }
                }

            }

            transform.position += transform.up * vSpeed * Time.deltaTime;

            if (transform.position.y > 2.0f)
            {
                burnTime -= Time.deltaTime;
                fuelPercent = (burnTime / totalBurnTime);
            }

            flying = fuelPercent > 0.0f;

            if (!flying)
            {
                UI.FadeOutIn();
            }
        }
        else if (!shop.Flying)
        {
            // bobbing motion in shop

            transform.position = new Vector3(transform.position.x, Mathf.Sin(Time.time) * 0.2f, transform.position.z);
        }

        //if (!isFalling)
        //{
        //    //If we haven't reached our destination
        //    if (transform.position.y < destination)


        //        if (transform.position.y > range)
        //        {
        //            aboveTarget = true;
        //        }
        //}

        //if (belowTarget)
        //{
        //    StartCoroutine(Fall());
        //}

        //if (aboveTarget)
        //{
        //    StartCoroutine(Fall());
        //}
    }

    IEnumerator Accelerate()
    {
        float t = 0;
        currentLerpTime = 0.0f;

        while (t < 1.0f)
        {
            //Increment timer once per frame
            currentLerpTime += Time.deltaTime;

            //Begin to lerp
            t = currentLerpTime / timeToMaxSpeed;
            t = 1.0f - Mathf.Cos(t * Mathf.PI * 0.5f);

            vSpeed = Mathf.Lerp(0, maxSpeed, t);

            yield return null;
        }

        float totalRange = range + destination;
    }

    IEnumerator Fall()
    {
        isFalling = true;

        //Fall
        float c = 0;
        currentLerpTime = 0.0f;

        while (c < 1.0f)
        {
            //Increment timer once per frame
            currentLerpTime += Time.deltaTime;

            //Begin to lerp
            c = currentLerpTime / timeToMaxSpeed;
            c = 1.0f - Mathf.Cos(c * Mathf.PI * 0.5f);

            //Move up
            vSpeed = -Mathf.Lerp(0, 10, c);

            yield return null;
        }
    }

    public void TakeOff()
    {
        //Launch
        Jcamera.HasTakenOff = true;
        dyingOffScreenCheck = false;
        flying = true;

        fuelPercent = 1.0f;
        totalBurnTime = burnTime;
        StartCoroutine(Accelerate());
    }

    public void HitGoal()
    {
        if (!isFalling)
        {
            Invoke("DelayedFalling", disableThrustDelay);
        }
        else
        {
            StopAllCoroutines();
            flying = false;

            UI.FadeOutIn();
            shop.ArrivedSafely();
        }
    }

    private void DelayedFalling()
    {
        StartCoroutine(Fall());
    }
}
