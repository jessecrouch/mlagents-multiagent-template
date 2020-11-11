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
    private Mesh mesh;
    private Bounds bounds;
    private float wallSize = 3f;
    public GameObject powerupPrefab;
    public GameObject arena;

    public override void Initialize()
    {
        userControl = GetComponentInChildren<ThirdPersonUserControl>();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Ray Perception Sensor is automatically collected
        // and no Vector Size needs to be added for it
        // Otherwise, use: sensor.AddObservation();
    }

    public override void OnActionReceived(float[] vectorAction)
    {
        // Every step, encourage the Agent to move
        AddReward(-0.01f);

        // Map vectorAction, which will either be from Heuristic() or 
        // behavior inference to the input variables so that we can feed
        // them to the ThirdPersonUserControl.cs->FixedUpdate()
        userControl.verticalInput = vectorAction[0];
        userControl.horizontalInput = vectorAction[1];
    }

    public override void OnEpisodeBegin()
    {
        // Set a random spawn position within the bounds
        Vector3 playerRandPos = GenerateRandomPosition(-1.3f);
        Vector3 powerupRandPos = GenerateRandomPosition(0.5f);
        transform.localPosition = playerRandPos;

        // Randomly spawn a powerup
        GameObject powerup = Instantiate(powerupPrefab) as GameObject;
        powerup.transform.parent = arena.transform;
        powerup.transform.localPosition = powerupRandPos;

        // Debug.Log("PlayerRandPos: " + playerRandPos);
        // Debug.Log("PowerupRandPos: " + powerupRandPos);
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
    private Vector3 GenerateRandomPosition(float yPos)
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
        Vector3 randomPos = new Vector3(Random.Range(bounds.min.x, bounds.max.x), yPos, Random.Range(bounds.min.z, bounds.max.z));
        // Debug.Log("Randompos: " + randomPos);

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
