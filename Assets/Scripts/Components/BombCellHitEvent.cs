using UnityEngine;

namespace RedDev.Game {
    struct BombCellHitEvent
    {
	    public Coords coords;

	    public BombCellHitEvent(Vector3Int cell)
	    {
			coords = new Coords(){value = cell};
	    }
    }
}