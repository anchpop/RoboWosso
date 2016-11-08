using UnityEngine;
using System.Collections;

public class TileController : MonoBehaviour {

    public bool impassable = false;
    protected GameController gcont;

    // Use this for initialization
    void Start ()
    {
        gcont = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        gcont.registerTile(this);

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
