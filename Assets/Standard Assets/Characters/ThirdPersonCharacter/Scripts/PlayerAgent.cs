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

    public override void Initialize()
    {
        userControl = GetComponentInChildren<ThirdPersonUserControl>();
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
    }

    public override void Heuristic(float[] actionsOut)
    {
        actionsOut[0] = CrossPlatformInputManager.GetAxis("Vertical"); // forward
        actionsOut[1] = CrossPlatformInputManager.GetAxis("Horizontal"); // sideways
        // actionsOut[2] = Input.GetKey(KeyCode.C); // crouch
    }
}
