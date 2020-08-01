using System.Collections.Generic;
using RedDev.Game.Tiles;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace RedDev.Game
{
	public class GridInfo : MonoBehaviour
	{
		public static GridInfo instance { get; private set; }

		public static readonly string GAMEFIELD_TAG = "Gamefield";
		public static readonly string BREAKEFIELD_TAG = "Breakefield";

		private Dictionary<Vector3Int, BlockData> _typesTiles = new Dictionary<Vector3Int, BlockData>();
		private List<Vector3Int> _deletedCells = new List<Vector3Int>();

		public BlockData this[Vector3Int cell] => _typesTiles.ContainsKey(cell) ? _typesTiles[cell] : null;

		public Tilemap tilemap { get; private set; }
		public Tilemap breakemap { get; private set; }

		void Start()
		{
			instance = this;
			var tilemapObj = GameObject.FindGameObjectWithTag(GAMEFIELD_TAG);
			if (tilemapObj == null)
			{
				Debug.LogError($"Not found tilemap with tag {GAMEFIELD_TAG} for {gameObject.name}");
				return;
			}
			tilemap = tilemapObj.GetComponent<Tilemap>();

			var breakeObj = GameObject.FindGameObjectWithTag(BREAKEFIELD_TAG);
			if (breakeObj == null)
			{
				Debug.LogError($"Not found breakemap with tag {BREAKEFIELD_TAG} for {gameObject.name}");
				return;
			}
			breakemap = breakeObj.GetComponent<Tilemap>();

			foreach (Vector3Int cell in tilemap.cellBounds.allPositionsWithin)
			{
				var tile = tilemap.GetTile<BaseBlockTile>(cell);
				if (tile == null)
					continue;
				Add(tile, cell);
			}
		}

		public void Clear()
		{
			_typesTiles.Clear();
			_deletedCells.Clear();
			//counterDestroyedBlocks = 0;
		}

		public void Add(BaseBlockTile tile, Vector3Int place)
		{
			if (!_typesTiles.ContainsKey(place))
				_typesTiles.Add(place, new BlockData(tilemap, breakemap, tile, place));
		}

		public void Remove(Vector3Int place)
		{
			_typesTiles.Remove(place);
		}

		public void DestroyTile(Vector3Int place)
		{
			if (!_deletedCells.Contains(place))
			{
				//counterDestroyedBlocks++;
				_deletedCells.Add(place);
			}
		}

		void LateUpdate()
		{
			var list = new List<Vector3Int>(_deletedCells);
			_deletedCells.Clear();
			foreach (var place in list)
			{
				var tile = this[place];
				if (tile != null)
				{
					tilemap.SetTile(place, null);
					breakemap.SetTile(place, null);
				}
				Remove(place);
			}
			_deletedCells.Clear();
		}
	}
}