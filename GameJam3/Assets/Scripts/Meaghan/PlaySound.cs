using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    #region Inspector Variables
    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip buttonClip;

    [SerializeField]
    private AudioClip fireButtonClip;
    #endregion


    public void PlayButtonSound()
    {
        audioSource.PlayOneShot(buttonClip, 0.5f);
    }

    public void PlayFireButtonSound()
    {
        audioSource.PlayOneShot(fireButtonClip, 0.5f);
    }




}
