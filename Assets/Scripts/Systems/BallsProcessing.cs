using Leopotam.Ecs;
using RedDev.Game.Tiles;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace RedDev.Game {
    sealed class BallsProcessing : IEcsInitSystem, IEcsRunSystem, IEcsDestroySystem
    {
	    private const string ROCKET_TAG = "Player";
	    private const string BALL_TAG = "Ball";
	    private const float SPEED = 10f;

	    private readonly EcsWorld _world = null;
	    private Tilemap gamefield;
		private EcsFilter<Ball, Speed> _ballsFilter = null;
	    private EcsFilter<Ball, Collision2DEnterEvent> _ballsCollisionsFilter = null;
	    private EcsFilter<Ball, Collision2DEnterEvent, DeadZoneEvent> _deadZonesFilter = null;

	    public void Init () {
	        foreach (var obj in GameObject.FindGameObjectsWithTag(BALL_TAG))
	        {
		        var body = obj.GetComponent<Rigidbody2D>();
		        var collBehaviour = obj.GetComponent<EcsCollisionBehaviour>();
				var ball = _world.NewEntity();
				ball
			        .Replace(new Ball())
			        .Replace(new RigidBodyComponent() { body = body })
			        .Replace(new Speed() { speed = SPEED });
		        body.velocity = Vector2.up * SPEED;
		        collBehaviour.currentEntity = ball;
	        }
		}

        public void Run()
		{
	        foreach (var i in _ballsCollisionsFilter)
	        {
				Vector3 hitPos = Vector3.zero;
				Vector3Int lastCell = default;
				ref var collision = ref _ballsCollisionsFilter.Get2(i);
				foreach (var hit in collision.contacts)
				{
					hitPos.x = hit.point.x - 0.01f * hit.normal.x;
					hitPos.y = hit.point.y - 0.01f * hit.normal.y;
					var cell = gamefield.WorldToCell(hitPos);
					if (lastCell == cell)
						continue;
					lastCell = cell;
					var tile = gamefield.GetTile<BaseBlockTile>(cell);
					switch (tile)
					{
						case RedBombBlockTile bomb:
							_world.NewEntity().Replace(new BombCellHitEvent(cell));
							break;
						case GreenBlockTile normal:
							_world.NewEntity().Replace(new NormalCellHitEvent(cell));
							break;
					}
				}
			}
		}

        public void Destroy()
        {
        }
    }
}