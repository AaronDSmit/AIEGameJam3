using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    //YAY FLIPPING



    [SerializeField]
    private float burnTimeReduction;

    [SerializeField]
    private float minX;

    [SerializeField]
    private float maxX;

    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private AudioClip collisionClip;

    private Collider2D myCollider;

    private float direction;
    private AudioSource audioSource;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        myCollider = GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        direction = 0;

        Invoke("StartMoving", Random.Range(0, 5.0f));
    }

    private void StartMoving()
    {
        direction = (Random.value > 0.5f) ? 1 : -1;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<JetPack>())
        {
            if(collisionClip != null)
            {
                audioSource.PlayOneShot(collisionClip, 1.0f);
            }

            other.gameObject.GetComponent<JetPack>().ReduceBurnTime(burnTimeReduction);

            Invoke("DelayDestroy", collisionClip.length);
        }
    }

    private void DelayDestroy()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (direction == 1) // moving right
        {
            if (transform.position.x >= maxX)
            {
                direction = -1;
                spriteRenderer.flipX = true;
            }
        }
        else if (direction == -1)// moving Left
        {
            if (transform.position.x <= minX)
            {
                direction = 1;
                spriteRenderer.flipX = false;
            }
        }

        transform.Translate(direction * moveSpeed * Time.deltaTime, 0.0f, 0.0f);
    }
}