using Leopotam.Ecs;
using RedDev.Game.Tiles;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace RedDev.Game {
    sealed class ObstacleProcessing : IEcsInitSystem, IEcsDestroySystem
    {
        public Tilemap gamefield;
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<Obstacle> _obstacles = null;
        
        public void Init () {
	        foreach (Vector3Int cell in gamefield.cellBounds.allPositionsWithin)
	        {
		        var tile = gamefield.GetTile<BaseBlockTile>(cell);
		        if (tile != null)
			        continue;
		        _world.NewEntity()
			        .Replace(new Obstacle(){coords = new Coords(){ value = cell }});
            }
        }

        void IEcsDestroySystem.Destroy()
        {
            foreach (var i in _obstacles)
                _obstacles.GetEntity(i).Destroy();
        }
    }
}