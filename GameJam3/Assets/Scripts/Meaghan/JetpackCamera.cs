using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetpackCamera : MonoBehaviour
{
    #region Inspector Variables
    [Tooltip("The offset for the Y on the camera.")]
    [SerializeField]
    private float yOffset;
    [Tooltip("The initial delay for the camera.")]
    [SerializeField]
    private float initialDelay = 2.0f;
    [Tooltip("The delay for the camera going back to it's original position.")]
    [SerializeField]
    private float originDelay = 2.0f;
    #endregion

    #region Variables
    private float t = 0.0f;
    private float currentLerpTime;
    private Vector3 startPos;
    private bool hasTakenOff = false;
    private bool hasLanded = false;
    private Transform target;
    #endregion

    #region Getters and Setters
    public bool HasTakenOff
    {
        get { return hasTakenOff; }

        set { hasTakenOff = value; }
    }

    public bool HasLanded
    {
        get { return hasLanded; }

        set { hasLanded = value; }
    }
    #endregion


    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!hasLanded)
        {
            if (hasTakenOff && t < 1 && target.position.y > 3.5f)
            {
                ////Increment timer once per frame
                currentLerpTime += Time.deltaTime;

                //Begin to lerp
                t = currentLerpTime / initialDelay;
                t = 1.0f - Mathf.Cos(t * Mathf.PI * 0.5f); // AB Testing

                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, target.position.y + yOffset, transform.position.z), t);

                ////Lerp delay for takeoff
                //transform.position = new Vector3(transform.position.x, Mathf.Lerp(0, (target.position.y + yOffset), t), transform.position.z);
            }
            else if (hasTakenOff && t >= 1)
            {
                //Follow player
                transform.position = new Vector3(transform.position.x, target.position.y + yOffset, transform.position.z);
            }
        }
        else
        {
            Invoke("ResetCamera", originDelay);
        }
    }

    public void ResetCamera()
    {
        currentLerpTime = 0.0f;
        transform.position = startPos;
        t = 0;
        hasTakenOff = false;
        hasLanded = false;
    }
}
