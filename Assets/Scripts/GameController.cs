using UnityEngine;
using System.Collections;
using CreativeSpore.SuperTilemapEditor;

public enum Direction { North, NorthEast, East, SouthEast, South, SouthWest, West, NorthWest }

public class GameController : MonoBehaviour {



    Renderer rend;
    Rigidbody2D body;

    public Tilemap map;

	// Use this for initialization
	void Start ()
    {
        rend = GetComponent<Renderer>();
        body = GetComponent<Rigidbody2D>();
    }

    public Vector3 getBoardPosition(Vector2 vec)
    {
        return new Vector2(Mathf.Floor(vec.x), Mathf.Floor(vec.y));
    }

    public bool checkIfTilePassable(Vector3 pos)
    {
        var a = map.GetComponent<Tilemap>();
        return !a.GetTile((int)getBoardPosition(pos).x, (int)getBoardPosition(pos).y).paramContainer.GetBoolParam("Impassable");
    }

    public Vector3 transformPosition(Vector3 basePos, Direction dir)
    {
        if (dir == Direction.North) basePos += new Vector3(0, 1, 0);
        else if (dir == Direction.South) basePos += new Vector3(0, -1, 0);
        else if (dir == Direction.East) basePos += new Vector3(1, 0, 0);
        else if (dir == Direction.West) basePos += new Vector3(-1, 0, 0);
        else if (dir == Direction.NorthEast) basePos += new Vector3(1, 1, 0);
        else if (dir == Direction.NorthWest) basePos += new Vector3(-1, 1, 0);
        else if (dir == Direction.SouthEast) basePos += new Vector3(1, -1, 0);
        else if (dir == Direction.SouthWest) basePos += new Vector3(-1, -1, 0);
        return basePos;
    }

    // Update is called once per frame
    void Update () {
	
	}
}
