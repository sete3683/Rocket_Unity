using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingPadCollision : MonoBehaviour
{
    [SerializeField] private ParticleSystem landingPadParticle;

    bool isPlayed = false;

    void OnCollisionEnter()
    {
        if (!isPlayed)
        {
            landingPadParticle.Play();
            isPlayed = true;
        }
    }
}
