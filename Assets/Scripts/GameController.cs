using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using CreativeSpore.SuperTilemapEditor;

public enum Direction { North, NorthEast, East, SouthEast, South, SouthWest, West, NorthWest }

public class GameController : MonoBehaviour {

    public Tilemap map;
    public List<GameObject> tiles;
    public Dictionary<Vector3, TileController> tilesDict = new Dictionary<Vector3, TileController>();

	// Use this for initialization
	void Start ()
    {
        tiles = GameObject.FindGameObjectsWithTag("Tile").ToList();
    }

    public void clearBoardColors()
    {
        foreach(var tile in tiles)
        {
            tile.GetComponent<SpriteRenderer>().color = Color.white;
        }
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


    public void registerTile(TileController t)
    {
        tilesDict.Add(t.transform.position, t);
    }

    public static Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles)
    {
        var dir = point - pivot; // get point direction relative to pivot
        dir = Quaternion.Euler(angles) * dir; // rotate it
        point = dir + pivot; // calculate rotated point
        return point; // return it
    }

    public static float Angle(Vector3 p_vector2)
    {
        if (p_vector2.x < 0)
        {
            return 360 - (Mathf.Atan2(p_vector2.x, p_vector2.y) * Mathf.Rad2Deg * -1);
        }
        else
        {
            return Mathf.Atan2(p_vector2.x, p_vector2.y) * Mathf.Rad2Deg;
        }
    }

}
