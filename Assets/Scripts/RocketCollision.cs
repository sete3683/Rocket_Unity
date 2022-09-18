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
        switch (collision.gameObject.tag)
        {
            case "Finish":
                if (!isEnded) StartCoroutine(Clear());
                break;

            case "Obstacle":
                if (!isEnded) StartCoroutine(Crash());
                break;
        }
    }
    
    IEnumerator Clear()
    {
        isEnded = true;
        rocketControl.enabled = false;
        rocketAudioSource.Stop();
        rocketAudioSource.PlayOneShot(rocketClearSound);

        yield return new WaitForSeconds(rocketClearDelay);

        isEnded = false;
        NextScene();
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
        ReloadScene();
    }

    void NextScene()
    {
        SceneManager.LoadScene(
            (SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings);
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
