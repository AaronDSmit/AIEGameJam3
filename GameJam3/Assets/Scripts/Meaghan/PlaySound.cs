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

    [SerializeField]
    private AudioClip winClip;

    [SerializeField]
    private AudioClip loseClip;

    [SerializeField]
    private AudioClip animalNoise1;

    [SerializeField]
    private AudioClip animalNoise2;

    [SerializeField]
    private AudioClip animalNoise3;
    #endregion


    public void PlayButtonSound()
    {
        audioSource.PlayOneShot(buttonClip, 0.5f);
    }

    public void PlayFireButtonSound()
    {
        audioSource.PlayOneShot(fireButtonClip, 0.5f);
    }

    public void PlayWinSound()
    {
        audioSource.PlayOneShot(winClip, 0.5f);
    }

    public void PlayLoseSound()
    {
        audioSource.PlayOneShot(loseClip, 0.5f);
    }

    void RandomCreatureNoise()
    {
        int select = Random.Range(0, 2);

        if(select == 0)
        {
            audioSource.PlayOneShot(animalNoise1, 0.5f);
        }
        else if(select == 1)
        {
            audioSource.PlayOneShot(animalNoise2, 0.5f);
        }
        else if (select == 2)
        {
            audioSource.PlayOneShot(animalNoise3, 0.5f);
        }
    }
}
