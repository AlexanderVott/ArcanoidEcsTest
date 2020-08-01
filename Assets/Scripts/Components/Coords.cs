using UnityEngine;

namespace RedDev.Game {
    struct Coords
    {
	    public Vector3Int value;

	    public override string ToString()
	    {
		    return value.ToString();
	    }
    }
}