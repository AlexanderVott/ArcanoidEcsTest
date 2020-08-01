using UnityEngine;

namespace RedDev.Game {
    struct Obstacle
    {
	    public Coords coords;
        
        public Obstacle(int x, int y)
        {
	        coords = new Coords { value = new Vector3Int(x, y, 0) };
        }
    }
}