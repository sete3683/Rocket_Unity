using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMovement : MonoBehaviour
{
    [SerializeField] private float rocketBoostSpeed = 0f;
    [SerializeField] private float rocketRotateSpeed = 0f;
    [SerializeField] private AudioClip rocketBoostSound;
    [SerializeField] private ParticleSystem rocketBoostParticle;

    private Rigidbody rocketRigidbody;
    private AudioSource rocketAudioSource;
    private float rotateDirection;

    private bool canBoost;

    void Start()
    {
        rocketRigidbody = GetComponent<Rigidbody>();
        rocketAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        CheckBoost();

        if (canBoost)
            PlayBoostEffects();
        else
            StopBoostEffects();

        CheckRotate();
    }

    void FixedUpdate()
    {
        if (canBoost)
            AddBoostPower();

        if (rotateDirection != 0f)
            AddRotatePower();
    }

    void OnCollisionEnter()
    {
        if (rocketBoostParticle.isPlaying)
            rocketBoostParticle.Stop();
    }

    #region Inner Logic

    void CheckBoost()
    {
        canBoost = Input.GetKey(KeyCode.Space);
    }

    void CheckRotate()
    {
        rotateDirection = -Input.GetAxisRaw("Horizontal");
    }

    void PlayBoostEffects()
    {
        if (!rocketAudioSource.isPlaying)
            rocketAudioSource.PlayOneShot(rocketBoostSound);

        if (!rocketBoostParticle.isPlaying)
            rocketBoostParticle.Play();
    }

    void StopBoostEffects()
    {
        if (rocketAudioSource.isPlaying)
            rocketAudioSource.Stop();

        if (rocketBoostParticle.isPlaying)
            rocketBoostParticle.Stop();
    }

    void AddBoostPower()
    {
        rocketRigidbody.AddRelativeForce(0, rocketBoostSpeed, 0);

    }

    void AddRotatePower()
    {
        rocketRigidbody.constraints ^= RigidbodyConstraints.FreezeRotationZ;
        rocketRigidbody.MoveRotation(
            rocketRigidbody.rotation * Quaternion.Euler(0, 0, rotateDirection * rocketRotateSpeed));
        rocketRigidbody.constraints ^= RigidbodyConstraints.FreezeRotationZ;
    }
    
    #endregion
}
