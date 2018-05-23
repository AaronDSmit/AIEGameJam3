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

    [SerializeField]
    private AudioClip takeOffSound;

    [SerializeField]
    private AudioClip flyingSound;

    #endregion

    #region Variables
    private float vSpeed = 0.0f;
    private float burnTime = 0.0f;
    private float currentLerpTime = 0.0f;
    private float destination;
    private Shop shop;
    private JetpackCamera Jcamera;

    private Customer customer;
    private bool flying = false;
    private bool isFalling = false;
    private float turningAngle;
    private float fuelPercent = 1.0f;
    private float totalBurnTime;
    private bool dyingOffScreenCheck;
    private AudioSource audioSource;
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

        audioSource = GetComponent<AudioSource>();
        customer = GetComponent<Customer>();

        startPos = transform.position;
        startRot = transform.rotation;
    }

    private void DieOffScreen()
    {
        if (transform.position.x < minX || transform.position.x > maxX)
        {
            shop.PlayerDied();
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

    public void ReduceBurnTime(float percent)
    {
        burnTime -= (burnTime * percent);
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

            if (transform.position.y > minHeightControl)
            {
                customer.StopSmokeEffect();

                if (Input.GetMouseButtonDown(0))
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
                }
            }

            transform.position += transform.up * vSpeed * Time.deltaTime;

            if (transform.position.y > 2.0f && !isFalling)
            {
                burnTime -= Time.deltaTime;
                fuelPercent = (burnTime / totalBurnTime);
            }

            flying = fuelPercent > 0.0f;

            if (!flying)
            {
                // shop.PlayerDied();
            }
        }
        else if (!shop.Flying)
        {
            // bobbing motion in shop

            transform.position = new Vector3(transform.position.x, startPos.y + Mathf.Sin(Time.time) * 0.2f, transform.position.z);
        }
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

        float vSpeedWhenFalling = vSpeed;

        if (audioSource != null)
        {
            //Stop playing the sound
            audioSource.Stop();
        }

        //Fall
        float c = 0;
        currentLerpTime = 0.0f;

        while (c < 1.0f)
        {
            //Increment timer once per frame
            currentLerpTime += Time.deltaTime;

            //Begin to lerp
            c = currentLerpTime / 1.0f;
            c = 1.0f - Mathf.Cos(c * Mathf.PI * 0.5f);

            //Move up
            vSpeed = Mathf.Lerp(vSpeedWhenFalling, -5, c);

            yield return null;
        }
    }

    public void TakeOff()
    {
        //Launch
        Jcamera.HasTakenOff = true;
        dyingOffScreenCheck = false;
        flying = true;
        isFalling = false;

        fuelPercent = 1.0f;
        totalBurnTime = burnTime;


        if (audioSource != null)
        {
            if (takeOffSound != null)
            {
                //Play launch sound
                audioSource.PlayOneShot(takeOffSound, 0.5f);

                Invoke("PlayLoopingSound", takeOffSound.length);
            }
        }

        StartCoroutine(Accelerate());
    }

    private void PlayLoopingSound()
    {
        if (flyingSound != null)
        {
            audioSource.clip = flyingSound;
            audioSource.Play();
            audioSource.loop = true;
        }
    }

    public void HitGoal()
    {
        if (!isFalling)
        {
            Invoke("DelayedFalling", disableThrustDelay);
        }
        else
        {
            // Second time we hit the goal, aka when falling

            StopAllCoroutines();
            flying = false;

            shop.ArrivedSafely();
        }
    }

    private void DelayedFalling()
    {
        StartCoroutine(Fall());
    }
}
