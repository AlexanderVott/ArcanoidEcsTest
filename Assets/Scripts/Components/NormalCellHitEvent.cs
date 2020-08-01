using UnityEngine;

namespace RedDev.Game {
    struct NormalCellHitEvent
    {
	    public Coords coords;

	    public NormalCellHitEvent(Vector3Int cell)
	    {
			coords = new Coords() { value = cell };
	    }
    }
}