using UnityEngine;
using UnityEngine.Tilemaps;

namespace RedDev.Game.Tiles
{
	public class BaseBlockTile : Tile
	{
		[SerializeField]
		protected Sprite _defaultSprite;
		public Sprite defaultSprite => _defaultSprite;
		
		public Tile[] _brokeSprites;

		public int health = 1;

		public Tile GetBrokenSprite(GridInfo info, Vector3Int position, BlockData data)
		{
			if (_brokeSprites.Length == 0)
				return null;
			int index = ((health - data.health) * _brokeSprites.Length) / health;
			if (index < _brokeSprites.Length)
				return _brokeSprites[index];
			return null;
		}
	}
}