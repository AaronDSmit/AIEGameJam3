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
    [Tooltip("The target to follow.")]
    [SerializeField]
    private Transform target;
    [Tooltip("The target to stop at.")]
    [SerializeField]
    private Transform goalTarget;
    [Tooltip("The delay for the camera going back to it's original position.")]
    [SerializeField]
    private float originDelay = 2.0f;
    #endregion

    #region Variables
    private float t = 0.0f;
    private float currentLerpTime;
    private bool isAtGoal;
    private bool getCurrentY = true;
    private float currY;
    private Transform startPos;
    private bool hasTakenOff = false;
    private bool hasLanded = false;
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

    private void Start()
    {
        startPos = transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!hasLanded)
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
            else if (hasTakenOff && t >= 1)
            {
                //Follow player
                transform.position = new Vector3(transform.position.x, target.position.y + yOffset, transform.position.z);
            }
        }
        else
        {
            Invoke("ToOrigin", originDelay);
        }
    }

    void ToOrigin()
    {
        transform.position = startPos.position;
    }

}
