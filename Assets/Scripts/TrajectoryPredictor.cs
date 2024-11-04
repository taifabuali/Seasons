using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryPredictor : MonoBehaviour
{
    public int predictionSteps = 30;          
    public float timeStep = 0.1f;               
    public LineRenderer lineRenderer;          

    void Awake()
    {
        if (lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
        }

        lineRenderer.positionCount = predictionSteps;
        lineRenderer.enabled = false;
    }

    public void PredictTrajectory(ProjectileProperties properties)
    {
        Vector3 currentPosition = properties.initialPosition;
        Vector3 currentVelocity = properties.direction * properties.initialSpeed;
        lineRenderer.enabled = true;

        lineRenderer.positionCount = predictionSteps;
        lineRenderer.SetPosition(0, currentPosition);

        for (int i = 1; i < predictionSteps; i++)
        {
            currentVelocity += Physics.gravity * timeStep;
            currentVelocity *= 1.0f - properties.drag * timeStep;
            currentPosition += currentVelocity * timeStep;

            lineRenderer.SetPosition(i, currentPosition);
        }
    }

    public void SetTrajectoryVisible(bool visible)
    {
        lineRenderer.enabled = visible;
    }
}

public struct ProjectileProperties
{
    public Vector3 initialPosition;
    public Vector3 direction;
    public float initialSpeed;
    public float mass;
    public float drag;
}