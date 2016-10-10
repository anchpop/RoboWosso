using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PlayerController : EntityController
{
    Dictionary<string, Direction> movements;
	// Use this for initialization
	public new void Start () {
        base.Start();
	}


    protected new void Update()
    {
        base.Update();
    }

    // Update is called once per frame
    protected override void OnSquareUpdate()
    {
        base.OnSquareUpdate();
        /*movements = new Dictionary<List<string>, Direction>()
        {
            {["w", Direction.North },
        };*/

        movements = new Dictionary<string, Direction>()
        {
            { "w",  Direction.North },
            { "s",  Direction.South },
            { "d",  Direction.East },
            { "a",  Direction.West },
            { "up",  Direction.North },
            { "down",  Direction.South },
            { "left",  Direction.West },
            { "right",  Direction.East },
        };

        foreach (KeyValuePair<string, Direction> entry in movements)
        {
            if (Input.GetKeyDown(entry.Key))
                attemptToMove(entry.Value);
        }
    }
}
