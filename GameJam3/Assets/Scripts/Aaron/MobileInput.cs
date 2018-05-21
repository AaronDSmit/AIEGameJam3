using UnityEngine;

public class MobileInput : MonoBehaviour
{
    #region SerializeField Fields

    [SerializeField]
    private float requiredSwipeTime;

    [SerializeField]
    private float deadZoneRadius;

    #endregion

    #region Variables

    private static bool pressed;
    private static bool down;
    private static bool tap;

    private static bool swipeLeft;
    private static bool swipeRight;
    private static bool swipeDown;
    private static bool swipeUp;

    [SerializeField]
    private bool moveLeft;
    [SerializeField]
    private bool moveRight;
    private static Vector2 moveDelta;

    private static bool isDraging;
    private static bool isPinching;

    private static float startTouchTime;
    private static float holdDownTime;
    private static float swipeMagnitude;

    private static Vector2 startTouch;
    private static Vector2 startTouch2;
    private static Vector2 swipeDelta;

    #endregion

    private void Update()
    {
        tap = swipeLeft = swipeRight = swipeDown = swipeUp = pressed = moveLeft = moveRight = false;

        #region StandAlone Input

        if (Input.GetMouseButtonDown(0))
        {
            tap = true;
            down = true;
            isDraging = true;
            holdDownTime = 0.0f;

            startTouch = Input.mousePosition;
            startTouchTime = Time.unscaledTime;
        }
        else if (Input.GetMouseButton(0))
        {
            holdDownTime = Time.unscaledTime - startTouchTime;

            if (Input.mousePosition.x > startTouch.x)
            {
                //moveRight = true;
            }

            if (Input.mousePosition.x < startTouch.x)
            {
                moveLeft = true;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDraging = false;
            down = false;

            holdDownTime = 0.0f;

            if (swipeDelta.magnitude < deadZoneRadius)
            {
                pressed = true;
            }

            Reset();
        }

        #endregion

        #region Mobile Input

        if (Input.touchCount > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                tap = true;
                down = true;
                isDraging = true;
                holdDownTime = 0.0f;

                startTouch = Input.touches[0].position;
                startTouchTime = Time.unscaledTime;
            }
            else if (Input.touches[0].phase == TouchPhase.Stationary)
            {
                holdDownTime = Time.unscaledTime - startTouchTime;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                isDraging = false;
                down = false;
                holdDownTime = 0.0f;

                if (swipeDelta.magnitude < deadZoneRadius)
                {
                    pressed = true;
                }

                Reset();
            }

            if (Input.touches[1].phase == TouchPhase.Began)
            {
                isPinching = true;

                startTouch2 = Input.touches[1].position;
            }
            else if (Input.touches[1].phase == TouchPhase.Ended || Input.touches[1].phase == TouchPhase.Canceled)
            {
                isPinching = false;
            }
        }

        #endregion

        // Calculate distance of swipe

        #region Swipe Calculation

        swipeDelta = Vector2.zero;

        if (isDraging)
        {
            if (Input.touchCount > 0)
            {
                swipeDelta = Input.touches[0].position - startTouch;
            }
            else if (Input.GetMouseButton(0))
            {
                swipeDelta = (Vector2)Input.mousePosition - startTouch;
            }
        }

        // did we cross the deadZone?

        if (swipeDelta.magnitude > deadZoneRadius)
        {
            tap = false;

            if (Time.unscaledTime < startTouchTime + requiredSwipeTime)
            {
                swipeMagnitude = swipeDelta.magnitude;

                // Which Direction
                float x = swipeDelta.x;
                float y = swipeDelta.y;

                if (Mathf.Abs(x) > Mathf.Abs(y))
                {
                    if (x < 0)
                    {
                        swipeLeft = true;
                    }
                    else
                    {
                        swipeRight = true;
                    }
                }
                else
                {
                    if (y < 0)
                    {
                        swipeDown = true;
                    }
                    else
                    {
                        swipeUp = true;
                    }
                }
            }

            Reset();
        }

        #endregion

        #region Pinch Calculation

        if (Input.touchCount == 2)
        {
            ScreenCapture.CaptureScreenshot("*/File.png", 1);
        }

        #endregion
    }

    private void Reset()
    {
        startTouch = Vector2.zero;
        swipeDelta = Vector2.zero;
        isDraging = false;
    }

    #region Getters

    public static bool Tap { get { return tap; } }

    public static bool Pressed { get { return pressed; } }
    public static bool Down { get { return down; } }

    public static float HoldTime { get { return holdDownTime; } }
    public static float SwipeMagnitude { get { return swipeMagnitude; } }

    public static bool SwipedLeft { get { return swipeLeft; } }
    public static bool SwipedRight { get { return swipeRight; } }
    public static bool SwipedDown { get { return swipeDown; } }
    public static bool SwipedUp { get { return swipeUp; } }

    public static bool MovedRight { get { return swipeDelta.x > 0; } }

    public static bool MovedLeft { get { return swipeDelta.x < 0; } }

    #endregion
}