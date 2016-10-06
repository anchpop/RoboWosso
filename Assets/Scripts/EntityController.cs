using UnityEngine;
using System.Collections;

public class EntityController : MonoBehaviour {

    public GameController gcont;

	// Use this for initialization
	void Start () {
	
	}

    void truePosition()
    {

    }

    void MoveTo(Vector3 pos)
    {
        transform.position = pos;
    }
	
    public void attemptToMove (Direction dir)
    {
        var newpos = gcont.transformPosition(transform.position, dir);
        if (gcont.checkIfTilePassable(newpos))
        {
            MoveTo(newpos);
        }
    }

    

    // Update is called once per frame
    void Update () {
	
	}
}
