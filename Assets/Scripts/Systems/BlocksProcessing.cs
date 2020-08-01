using Leopotam.Ecs;
using RedDev.Game.Tiles;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace RedDev.Game {
    sealed class BlocksProcessing : IEcsInitSystem, IEcsRunSystem, IEcsDestroySystem
	{
	    public Tilemap gamefield;
	    private readonly EcsWorld _world = null;
	    private readonly EcsFilter<BlockTile, Health> _blocks = null;
		
	    public void Init()
	    {
		    foreach (Vector3Int cell in gamefield.cellBounds.allPositionsWithin)
		    {
			    var tile = gamefield.GetTile<BaseBlockTile>(cell);
			    if (tile == null)
				    continue;
			    _world.NewEntity()
				    .Replace(new BlockTile(cell.x, cell.y))
				    .Replace(new Health { health = tile.health });
		    }
		}

		void IEcsDestroySystem.Destroy()
	    {
		    foreach (var i in _blocks)
			    _blocks.GetEntity(i).Destroy();
		}

		public void Run()
		{

		}
	}
}