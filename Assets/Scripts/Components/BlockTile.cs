using UnityEngine;

namespace RedDev.Game {
	struct BlockTile
    {
	    public Coords coords;

	    public BlockTile(int x, int y)
	    {
		    coords = new Coords { value = new Vector3Int(x, y, 0) };
	    }
    }
}