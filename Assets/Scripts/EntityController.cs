using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public class EntityController : MonoBehaviour {

    protected GameController gcont;
    protected AStarPath pather;
    protected Renderer rend;
    public float speed = 3;
    public float lookTurnSpeed = 3;
    protected Vector3 lookingDirection = Vector3.up;

    public enum States { onSquare, jumping, doneWithRound }
    public States currentState;

    // Use this for initialization
    protected void Start ()
    {
        currentState = States.onSquare;
        pather = new AStarPath(getNeighbouringSquares);
        gcont = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        rend = GetComponent<Renderer>();
    }

    protected List<Vector3> getNeighbouringSquares(Vector3 from)
    {
        var directions = new List<Direction> { Direction.North, Direction.South, Direction.East, Direction.West };
        return directions.Select(h => gcont.transformPosition(from, h)).Where(h => gcont.checkIfTilePassable(h)).ToList();
    }


    // Update is called once per frame
    protected void Update()
    {
        if (currentState == States.onSquare)
            OnSquareUpdate();
        else if (currentState == States.jumping)
            JumpingUpdate();
        else if (currentState == States.doneWithRound)
            doneWithRoundUpdate();
    }
    
    virtual protected void OnSquareUpdate()
    {
    }
    
    virtual protected void JumpingUpdate()
    {
    }
    
    virtual protected void doneWithRoundUpdate()
    {
    }

    public Vector3 boardPosition()
    {
        return gcont.getBoardPosition(transform.position);
    }

    public Vector3 worldPosition()
    {
        return transform.position;
    }
    
    
	
    public void attemptToMove(Direction dir)
    {
        var newpos = gcont.transformPosition(worldPosition(), dir);
        attemptToMove(newpos);
    }

    public void attemptToMove(Vector3 pos)
    {
        if (gcont.checkIfTilePassable(pos))
        {
            StartCoroutine(JumpTo(pos));
        }
    }


    public void moveTo(Vector3 pos)
    {
        transform.position  = pos;
    }

    virtual public void whileMoving(Vector3 moveTo)
    {
       

       if (moveTo != transform.position)
            //lookingDirection = moveTo - transform.position;
            lookingDirection = Vector3.Lerp(lookingDirection, moveTo - transform.position, lookTurnSpeed * Time.deltaTime);
    }

    virtual public IEnumerator JumpTo(Vector3 pos, float jumpspeed = -1)
    {
        currentState = States.jumping;
        Vector3 originalpos = transform.position;
        rend.sortingOrder = 1;
        jumpspeed = jumpspeed == -1 ? speed : jumpspeed;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / ((originalpos - pos).magnitude / jumpspeed))
        {
            // position
            float lerp = 1f - Mathf.Cos(t * Mathf.PI * 0.5f);     // ease in
            lerp = Mathf.Sin(lerp * Mathf.PI * 0.5f);             // ease out
            moveTo(Vector3.Lerp(originalpos, pos, lerp));

            // scale 
            lerp = Mathf.PingPong(t * 2, 1);
            lerp = Mathf.Sin(lerp * Mathf.PI * 0.5f);
            transform.localScale = Vector3.Lerp(new Vector3(1f, 1f, 1f), new Vector3(1.3f, 1.3f, 1f), lerp);

            whileMoving(pos);

            yield return null;
        }
        
        moveTo(pos);
        rend.sortingOrder = 0;
        currentState = States.onSquare;
        yield break;
    }

    


}

