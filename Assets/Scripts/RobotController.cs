using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class RobotController : EntityController
{
    public List<Vector3> positions = new List<Vector3>();
    int currentRoundInPatrol = 0;
    public float visionDistance = 5;
    public float visionFOV = 45;
    int numOfVisionRaycasts = 30;
    public LayerMask tileMask;

    public List<Vector3> path;

    public Color visibleColor;
    

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
        if (path.Count == 0)
            currentState = States.doneWithRound;
        else
        {
            attemptToMove(path[0]);
            path.RemoveAt(0);
        }
        Debug.Log(lookingDirection);
    }

    override public void whileMoving(Vector3 moveTo)
    {
        base.whileMoving(moveTo);

        gcont.clearBoardColors();
        foreach (var tile in squaresVisible())
        {
            tile.GetComponent<SpriteRenderer>().color = visibleColor;
        }
    }

    List<GameObject> squaresVisible()
    {
        List<GameObject> squaresToReturn = new List<GameObject>();
        var lookingAngle = -GameController.Angle(lookingDirection);
        var startDir = lookingAngle - visionFOV;
        var endDir   = lookingAngle + visionFOV;

        var offsetEachCast   = visionFOV / numOfVisionRaycasts;
        for (float rayDir = startDir; rayDir < endDir; rayDir += offsetEachCast)
        {
            Vector3 vectDirect = Quaternion.AngleAxis(rayDir, Vector3.forward) * Vector3.up;
            var casts = Physics2D.RaycastAll(transform.position, vectDirect, visionDistance, tileMask);
            foreach(var raycastHit2D in casts)
            {
                var o = raycastHit2D.collider.gameObject;
                if (gcont.checkIfTilePassable(o.transform.position))
                {
                    if (!squaresToReturn.Contains(o)) squaresToReturn.Add(o);
                }
                else
                    break;
            }
        }
        return squaresToReturn;
    }
    
}
