using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Characters.ThirdPerson;

public class PlayerAgent : Agent
{
    private ThirdPersonUserControl userControl;
    public Transform ground;
    private float groundX;
    private float groundZ;
    private Mesh mesh;
    private Bounds bounds;
    private float wallSize = 1;

    public override void Initialize()
    {
        userControl = GetComponentInChildren<ThirdPersonUserControl>();

        // Set a random spawn position within the bounds
        transform.position = GenerateRandomPosition();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Ray Perception Sensor is automatically collected
        // and no Vector Size needs to be added for it
        // Otherwise, use: sensor.AddObservation();
    }

    public override void OnActionReceived(float[] vectorAction)
    {
        userControl.verticalInput = vectorAction[0];
        userControl.horizontalInput = vectorAction[1];
    }

    public override void OnEpisodeBegin()
    {
        // Set a random spawn position within the bounds
        transform.position = GenerateRandomPosition();
    }

    public override void Heuristic(float[] actionsOut)
    {
        actionsOut[0] = CrossPlatformInputManager.GetAxis("Vertical"); // forward
        actionsOut[1] = CrossPlatformInputManager.GetAxis("Horizontal"); // sideways
        // actionsOut[2] = Input.GetKey(KeyCode.C); // crouch
    }

    /// <summary>
    /// Generate a random position for the PlayerAgent or powerup
    /// </summary>
    /// <returns></returns>
    private Vector3 GenerateRandomPosition()
    {
        // Get the bounds of the arena's ground
        Mesh mesh = ground.GetComponent<MeshFilter>().mesh;
        Bounds bounds = mesh.bounds;
        // Debug.Log("X Bounds: " + (bounds.min.x, bounds.max.x));
        // Debug.Log("Z Bounds: " + (bounds.min.z, bounds.max.z));

        // Subtract the walls from the bounds
        bounds.SetMinMax(bounds.min + new Vector3(wallSize, 0, wallSize), bounds.max - new Vector3(wallSize, 0, wallSize));
        // Debug.Log("X Bounds minus wall: " + (bounds.min.x, bounds.max.x));
        // Debug.Log("Z Bounds minus wall: " + (bounds.min.x, bounds.max.x));

        // Set a random spawn position within the bounds
        Vector3 randomPos = new Vector3(Random.Range(bounds.min.x, bounds.max.x), 0, Random.Range(bounds.min.z, bounds.max.z));

        return randomPos;
    }

    public void GotPowerUp()
    {
        // Good job, Agent
        AddReward(1f);

        // By marking an agent as done AgentReset() will be called automatically.
        EndEpisode();
    }
}
