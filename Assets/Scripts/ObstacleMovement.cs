using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    private readonly float Tau = Mathf.PI * 2;

    [SerializeField] private Vector3 obstacleMovementVector;
    [SerializeField, Range(-1, 1)] private float obstacleMovementFactor;
    [SerializeField] private float cycleTime;
    private Vector3 obstacleStartPosition;

    void Start()
    {
        obstacleStartPosition = transform.position;
    }

    void Update()
    {
        float cycle = Time.time / cycleTime;
        float sin = Mathf.Sin(cycle * Tau);
        obstacleMovementFactor = sin;
        transform.position = obstacleStartPosition + obstacleMovementVector * obstacleMovementFactor;
    }
}
