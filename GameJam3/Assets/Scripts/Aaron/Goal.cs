using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private Collider2D myCollider;

    private void Awake()
    {
        myCollider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<JetPack>())
        {
            other.gameObject.GetComponent<JetPack>().HitGoal();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<JetPack>())
        {
            other.gameObject.GetComponent<JetPack>().HitGoal();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<JetPack>())
        {
            myCollider.isTrigger = false;
        }
    }
}