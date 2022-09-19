using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RocketCollision : MonoBehaviour
{
    [SerializeField] private float rocketClearDelay = 3f;
    [SerializeField] private float rocketCrashDelay = 3f;
    [SerializeField] private AudioClip rocketClearSound;
    [SerializeField] private AudioClip rocketCrashSound;
    [SerializeField] private ParticleSystem rocketClearParticle;
    [SerializeField] private ParticleSystem rocketCrashParticle;

    private RocketMovement rocketControl;
    private AudioSource rocketAudioSource;

    private bool isEnded = false;

    void Start()
    {
        rocketControl = GetComponent<RocketMovement>();
        rocketAudioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isEnded)
            return;

        switch (collision.gameObject.tag)
        {
            case "Finish":
                StartCoroutine(Clear());
                break;

            case "Obstacle":
                StartCoroutine(Crash());
                break;
        }
    }
    
    IEnumerator Clear()
    {
        isEnded = true;
        rocketControl.enabled = false;
        rocketAudioSource.Stop();
        rocketAudioSource.PlayOneShot(rocketClearSound);
        rocketClearParticle.Play();

        yield return new WaitForSeconds(rocketClearDelay);

        isEnded = false;
        SceneHandler.NextScene();
    }

    IEnumerator Crash()
    {
        isEnded = true;
        rocketControl.enabled = false;
        rocketAudioSource.Stop();
        rocketAudioSource.PlayOneShot(rocketCrashSound);
        rocketCrashParticle.Play();

        yield return new WaitForSeconds(rocketCrashDelay);

        isEnded = false;
        SceneHandler.ReloadScene();
    }
}
