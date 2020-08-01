using Leopotam.Ecs;
using RedDev.Game.Tiles;
using UnityEngine;

namespace RedDev.Game {
    sealed class BlocksHitsProcessing : IEcsRunSystem {

	    protected readonly EcsWorld _world;
	    private EcsFilter<NormalCellHitEvent> _normalHitsFilter = null;
		private EcsFilter<BombCellHitEvent> _bombHitsFilter = null;

        private GridInfo gridInfo;
		
		void IEcsRunSystem.Run ()
		{
			ProcessingBombsHits();
	        ProcessingNormalsHits();
        }

		private void DestroyCell(Vector3Int cell)
		{
			GridInfo.instance.DestroyTile(cell);
		}

        private void ProcessingNormalsHits()
        {
	        Vector3Int lastCell = default;
			foreach (var i in _normalHitsFilter)
	        {
		        var cell = _normalHitsFilter.Get1(i).coords.value;
		        if (lastCell == cell)
			        continue;
		        lastCell = cell;
		        var blockData = gridInfo[cell];
				var tile = blockData?.baseTile as GreenBlockTile;
		        if (tile == null)
			        continue;
		        if (--blockData.health <= 0) 
			        DestroyCell(cell);
		        else
		        {
					blockData.breakeMap.SetTile(cell, tile.GetBrokenSprite(gridInfo, cell, blockData));
					blockData.map.RefreshTile(cell);
		        }
	        }
        }

        private void ProcessingBombsHits()
		{
			Vector3Int lastCell = default;
			foreach (var i in _bombHitsFilter)
			{
				var cell = _bombHitsFilter.Get1(i).coords.value;
				if (lastCell == cell)
					continue;
				lastCell = cell;
				var blockData = gridInfo[cell];
				var tile = blockData?.baseTile as RedBombBlockTile;
				if (tile == null)
					continue;
				if (--blockData.health <= 0)
				{
					DestroyCell(cell);
					CrossBombing(cell);
				}
				else
				{
					blockData.breakeMap.SetTile(cell, tile.GetBrokenSprite(gridInfo, cell, blockData));
					blockData.map.RefreshTile(cell);
				}
			}
        }

        private void CrossBombing(Vector3Int cell)
        {
	        Vector3Int offsetCell;
	        for (int y = -1; y <= 1; y++)
	        {
		        for (int x = -1; x <= 1; x++)
		        {
			        offsetCell = cell + new Vector3Int(x, y, 0);
			        var otherBlock = gridInfo[offsetCell];
			        if (otherBlock == null)
				        continue;
			        var offsetTile = otherBlock.map.GetTile<BaseBlockTile>(offsetCell);
			        if (offsetTile != null)
			        {
				        switch (offsetTile)
				        {
					        case RedBombBlockTile bomb:
						        _world.NewEntity().Replace(new BombCellHitEvent(offsetCell));
						        break;
					        case GreenBlockTile normal:
						        _world.NewEntity().Replace(new NormalCellHitEvent(offsetCell));
						        break;
				        }
			        }
			        DestroyCell(offsetCell);
		        }
	        }
		}
    }
}