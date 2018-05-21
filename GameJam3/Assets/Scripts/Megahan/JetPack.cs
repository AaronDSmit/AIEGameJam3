using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetPack : MonoBehaviour
{
    #region Public Variables
    [Tooltip("Time it takes to lerp to destination.")]
    [SerializeField]
    private float lerpTime = 1.0f;
    [SerializeField]
    private ParticleSystem packParticle;
    [SerializeField]
    private ParticleSystem launchParticle;
    [Tooltip("The vitcory range.")]
    [SerializeField]
    private float range;
    #endregion

    #region Variables
    private float vSpeed = 0.0f;
    private float burnTime = 0.0f;
    private float weight = 0.0f;
    private float currentLerpTime = 0.0f;
    private float targetHeight;
    private float destination;
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

            //Move up
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(0, targetHeight, t), transform.position.z);

            yield return null;
        }

        float totalRange = range + destination;

        //If we haven't reached our destination
        if(targetHeight < destination || targetHeight > totalRange)
        {
            //Play the particle effect
            if (packParticle != null)
            {
                packParticle.Play();

                //Wait
                yield return new WaitForSeconds(packParticle.main.duration);

                //Stop the particle effect
                packParticle.Stop();

            }

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
    }

    public void TakeOff()
    {
        //Calculate target height
        targetHeight = (vSpeed - weight) * burnTime;

        if(launchParticle != null)
        {
            launchParticle.Play();
        }

        //Launch
        StartCoroutine("Move");
    }
}
