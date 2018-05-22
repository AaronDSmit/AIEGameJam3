using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondJetpack : MonoBehaviour
{
    #region Public Variables
    [Tooltip("Time it takes to lerp to destination.")]
    [SerializeField]
    private float lerpTime = 1.0f;
    [SerializeField]
    private ParticleSystem packParticle;
    [SerializeField]
    private ParticleSystem launchParticle;
    [SerializeField]
    private ParticleSystem explosionParticle;
    [Tooltip("The vitcory range.")]
    [SerializeField]
    private float range;


    private JetpackCamera Jcamera;
    #endregion

    #region Variables
    private float vSpeed = 0.0f;
    private float burnTime = 0.0f;
    private float weight = 0.0f;
    private float currentLerpTime = 0.0f;
    private float targetHeight;
    private float destination;
    private bool flying = false;
    private Shop shop;
    private bool belowTarget;
    private bool aboveTarget;
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

    public float Destination
    {
        get { return destination; }
        set { destination = value; }
    }
    #endregion


    [SerializeField]
    private float roationDegree;

    private void Awake()
    {
        shop = FindObjectOfType<Shop>();

        Jcamera = FindObjectOfType<JetpackCamera>();
    }

    private void Update()
    {
        if (flying)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Input.mousePosition.x < Screen.width / 2)
                {
                    // move left
                    transform.Rotate(Vector3.forward, roationDegree);
                }
                else
                {
                    // move right
                    transform.Rotate(Vector3.forward, -roationDegree);
                }
            }

            transform.position += transform.up * vSpeed * Time.deltaTime;
        }
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
            t = currentLerpTime / lerpTime;
            t = 1.0f - Mathf.Cos(t * Mathf.PI * 0.5f);

            vSpeed = Mathf.Lerp(0, 10, t);

            yield return null;
        }
    }

    IEnumerator Fall()
    {
        //Fall
        float c = 0;
        currentLerpTime = 0.0f;

        while (c < 1.0f)
        {
            //Increment timer once per frame
            currentLerpTime += Time.deltaTime;

            //Begin to lerp
            c = currentLerpTime / lerpTime;
            c = 1.0f - Mathf.Cos(c * Mathf.PI * 0.5f);

            //Move up
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(targetHeight, 0, c), transform.position.z);

            yield return null;
        }
    }

    public void TakeOff()
    {
        //Calculate target height
        targetHeight = (vSpeed - weight) * burnTime;

        if (launchParticle != null)
        {
            launchParticle.Play();
        }


        Jcamera.HasTakenOff = true;
        //Launch
        flying = true;
        StartCoroutine(Move());
    }
}
