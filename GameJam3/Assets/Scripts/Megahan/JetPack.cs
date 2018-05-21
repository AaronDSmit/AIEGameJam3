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
    #endregion

    #region Variables
    public float vSpeed = 0.0f;
    public float burnTime = 0.0f;
    public float weight = 0.0f;
    private bool launch = false;
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

        //Play the particle effect
        if(packParticle != null)
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

    public void TakeOff()
    {
        //Calculate target height
        targetHeight = (vSpeed - weight) * burnTime;

        //Launch
        StartCoroutine("Move");
    }
}
