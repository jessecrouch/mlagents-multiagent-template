﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The Area (SoccerFieldArea) stores the list of all agents in playerStates
// Agents add themselves to this list upon initialization
// 
[System.Serializable]
public class PlayerState
{
    public int playerIndex;
    public Rigidbody agentRb;
    public Vector3 startingPos;
    public PlayerAgent agentScript;
    public float ballPosReward;
}

public class ArenaController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
