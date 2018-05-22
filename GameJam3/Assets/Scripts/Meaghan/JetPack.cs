using System.Collections;
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
    }

    private void Update()
    {
        if (flying)
        {
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

            burnTime -= Time.deltaTime;

            if (burnTime / totalBurnTime < 0.95f)
            {
                fuelPercent = (burnTime / totalBurnTime) + 0.1f;
            }

            flying = burnTime > 0;
        }
        else
        {
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

    IEnumerator Move()
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
        flying = true;

        totalBurnTime = burnTime;
        StartCoroutine(Move());
    }


    private void OnTriggerEnter(Collider other)
    {
        if (isFalling)
        {
            if (other.tag == "Goal")
            {
                //Call user interface
                //Particles

                //Stop moving
                aboveTarget = false;
                shop.ArrivedSafely();
                Jcamera.HasLanded = true;
            }
        }

        if (other.tag == "Object")
        {
            if (!hasBurntFuel)
            {
                burnTime -= fuelReduction;
                hasBurntFuel = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Object")
        {
            hasBurntFuel = false;
        }
    }
}
