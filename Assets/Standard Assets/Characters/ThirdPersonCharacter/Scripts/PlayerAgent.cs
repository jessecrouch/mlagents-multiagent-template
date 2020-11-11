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

    public override void Initialize()
    {
        userControl = GetComponentInChildren<ThirdPersonUserControl>();

        // Get the bounds of the arena's ground, minus the walls
        Mesh mesh = ground.GetComponent<MeshFilter>().mesh;
        Bounds bounds = mesh.bounds;
        // Debug.Log("Bounds: " + (bounds.min.x, bounds.max.x));

        // Set a random spawn position within the bounds
        transform.localPosition = new Vector3(Random.Range(bounds.min.x, bounds.max.x), 0, Random.Range(bounds.min.z, bounds.max.z));
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // sensor.AddObservation();
    }

    public override void OnActionReceived(float[] vectorAction)
    {
        userControl.verticalInput = vectorAction[0];
        userControl.horizontalInput = vectorAction[1];
    }

    public override void OnEpisodeBegin()
    {
        // Get the bounds of the arena's ground, minus the walls
        Mesh mesh = ground.GetComponent<MeshFilter>().mesh;
        Bounds bounds = mesh.bounds;
        // Debug.Log("Bounds: " + (bounds.min.x, bounds.max.x));
        Debug.Log("begin");
        // Set a random spawn position within the bounds
        transform.localPosition = new Vector3(Random.Range(bounds.min.x, bounds.max.x), 0, Random.Range(bounds.min.z, bounds.max.z));
    }

    public override void Heuristic(float[] actionsOut)
    {
        actionsOut[0] = CrossPlatformInputManager.GetAxis("Vertical"); // forward
        actionsOut[1] = CrossPlatformInputManager.GetAxis("Horizontal"); // sideways
        // actionsOut[2] = Input.GetKey(KeyCode.C); // crouch
    }

    private void RandomPosition()
    {

    }

    public void GotPowerUp()
    {
        // Good job, Agent
        AddReward(1f);

        // By marking an agent as done AgentReset() will be called automatically.
        EndEpisode();
    }
}
