using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class RobotController : EntityController
{
    public List<Vector3> positions = new List<Vector3>();
    int currentRoundInPatrol = 0;

    public List<Vector3> path;
    

    // Use this for initialization
    public new void Start () {
        base.Start();
        List<Vector3> path = pather.findPath(transform.position, positions[0]);
        currentState = States.doneWithRound;

    }
	
	// Update is called once per frame
	public new void Update () {
        base.Update();
    }

    override protected void doneWithRoundUpdate()
    {
        base.OnSquareUpdate();
        path = pather.findPath(transform.position, positions[currentRoundInPatrol % positions.Count]);

        currentRoundInPatrol++;

        currentState = States.onSquare;

    }

    protected override void OnSquareUpdate()
    {
        base.OnSquareUpdate();
        if (path.Count <= 1)
            currentState = States.doneWithRound;
        else
        {
            attemptToMove(path[0]);
            path.RemoveAt(0);
        }
    }
    
}
