using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] randomClips;
    public AudioClip screamClip;
    public AudioClip attackClip;
    public AudioClip lostClip;

    public AudioClip safeClip;
    public AudioClip chaseClip;

    public void PlayRandomSound(AudioSource audioSource)
    {
        audioSource.clip = randomClips[Random.Range(0, randomClips.Length)];
        audioSource.PlayOneShot(audioSource.clip);
    }

    public void EnemyScream(AudioSource audioSource, GameObject music)
    {
        audioSource.clip = screamClip;
        audioSource.PlayOneShot(audioSource.clip);

        music.GetComponent<AudioSource>().clip = chaseClip;
        music.GetComponent<AudioSource>().Play();
    }

    public void EnemyInvestigating(AudioSource audioSource, GameObject music)
    {
        audioSource.clip = lostClip;
        audioSource.PlayOneShot(audioSource.clip);

        music.GetComponent<AudioSource>().clip = safeClip;
        music.GetComponent<AudioSource>().Play();
    }

    public void EnemyAttack(AudioSource audioSource)
    {
        audioSource.clip = attackClip;
        audioSource.PlayOneShot(audioSource.clip);
    }
}
