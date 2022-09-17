using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMovement : MonoBehaviour
{
    [SerializeField] private float boostSpeed = 0f;
    [SerializeField] private float rotateSpeed = 0f;
    private Rigidbody rocketRigidbody;
    private AudioSource rocketAudioSource;
    private bool canBoost;
    private float rotateDirection;

    // Start is called before the first frame update
    void Start()
    {
        rocketRigidbody = GetComponent<Rigidbody>();
        rocketAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckBoost();
        CheckRotate();
    }

    void FixedUpdate()
    {
        if (canBoost)
        {
            rocketRigidbody.AddRelativeForce(0, boostSpeed, 0);
        }

        if (rotateDirection != 0f)
        {
            rocketRigidbody.constraints ^= RigidbodyConstraints.FreezeRotationZ;
            rocketRigidbody.MoveRotation(
                rocketRigidbody.rotation * Quaternion.Euler(0, 0, rotateDirection * rotateSpeed));
            rocketRigidbody.constraints ^= RigidbodyConstraints.FreezeRotationZ;
        }
    }

    void CheckBoost()
    {
        canBoost = Input.GetKey(KeyCode.Space);

        if (canBoost)
        {
            if (!rocketAudioSource.isPlaying)
                rocketAudioSource.Play();
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
