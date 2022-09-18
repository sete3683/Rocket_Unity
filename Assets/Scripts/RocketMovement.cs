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
        CheckRotate();
    }

    void FixedUpdate()
    {
        if (canBoost)
        {
            rocketRigidbody.AddRelativeForce(0, rocketBoostSpeed, 0);
        }

        if (rotateDirection != 0f)
        {
            rocketRigidbody.constraints ^= RigidbodyConstraints.FreezeRotationZ;
            rocketRigidbody.MoveRotation(
                rocketRigidbody.rotation * Quaternion.Euler(0, 0, rotateDirection * rocketRotateSpeed));
            rocketRigidbody.constraints ^= RigidbodyConstraints.FreezeRotationZ;
        }
    }

    void CheckBoost()
    {
        canBoost = Input.GetKey(KeyCode.Space);

        if (canBoost)
        {
            if (!rocketAudioSource.isPlaying)
                rocketAudioSource.PlayOneShot(rocketBoostSound);

            rocketBoostParticle.Play();
        }
        else
        {
            if (rocketAudioSource.isPlaying)
                rocketAudioSource.Stop();
        }
    }

    void CheckRotate()
    {
        rotateDirection = -Input.GetAxisRaw("Horizontal");
    }
}
