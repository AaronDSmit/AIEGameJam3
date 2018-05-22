using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetpackCamera : MonoBehaviour
{
    #region Public Variables
    [Tooltip("The offset for the Y on the camera.")]
    [SerializeField]
    private float yOffset;
    [Tooltip("The initial delay for the camera.")]
    [SerializeField]
    private float initialDelay = 2.0f;
    [Tooltip("The target to follow.")]
    [SerializeField]
    private Transform target;
    [Tooltip("The target to stop at.")]
    [SerializeField]
    private Transform goalTarget;
    #endregion

    #region Variables
    private float t = 0.0f;
    private float currentLerpTime;
    private bool isAtGoal;
    private bool getCurrentY = true;
    private float currY;

    private bool hasTakenOff = false;
    #endregion

    #region Getters and Setters
    public bool HasTakenOff
    {
        get { return hasTakenOff; }

        set { hasTakenOff = value; }
    }
    #endregion

    // Update is called once per frame
    void LateUpdate()
    {
        if(transform.position.y >= goalTarget.position.y)
        {
            isAtGoal = true;
        }


        if (!isAtGoal)
        {
            if (hasTakenOff && t < 1)
            {
                //Increment timer once per frame
                currentLerpTime += Time.deltaTime;

                //Begin to lerp
                t = currentLerpTime / initialDelay;
                // t = 1.0f - Mathf.Cos(t * Mathf.PI * 0.5f); // AB Testing

                //Lerp delay for takeoff
                transform.position = new Vector3(transform.position.x, Mathf.Lerp(0, (target.position.y + yOffset), t), transform.position.z);
            }
            else
            {
                //Follow player
                transform.position = new Vector3(transform.position.x, target.position.y + yOffset, transform.position.z);
            }
        }
        else
        {
            //Stop at the y pos of the goal target
            transform.position = new Vector3(transform.position.x, goalTarget.position.y, transform.position.z);
        }
    }
}
