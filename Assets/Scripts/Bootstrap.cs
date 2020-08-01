using Leopotam.Ecs;
using RedDev.Game.Balls;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace RedDev.Game.Bootstrap
{
	public class Bootstrap : MonoBehaviour
	{
		private const string GAMEFIELD_TAG = "Gamefield";

		private EcsWorld _world;
		private EcsSystems _systems;
		private ArcanoidInput _input;

		void Awake()
		{
			_input = new ArcanoidInput();
			_input.Enable();
		}

		void Start()
		{
			var tilemapObj = GameObject.FindGameObjectWithTag(GAMEFIELD_TAG);
			if (tilemapObj == null)
			{
				Debug.LogError($"Not found tilemap with tag {GAMEFIELD_TAG}");
				return;
			}
			var tilemap = tilemapObj.GetComponent<Tilemap>();

			var gridInfo = GameObject.FindObjectOfType<GridInfo>();

			_world = new EcsWorld();
			_systems = new EcsSystems(_world);
#if UNITY_EDITOR
			Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(_world);
			Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_systems);
#endif

			/*BallCollisionEmitter.Initialize(_systems.World);
			_systems.OneFrame<BallCollision2DEvent>();*/

			_systems
				.Add(new ObstacleProcessing())
				.Add(new InputProcessing())
				.Add(new RocketProcessing())
				.Add(new BallsProcessing())
				.Add(new CollisionsLogSystem())
				.Add(new RocketCollisionProcessing())
				
				.Add(new BlocksHitsProcessing())
				.OneFrame<NormalCellHitEvent>()
				.OneFrame<BombCellHitEvent>()

				.Add(new DeadZonesProcessing())
				
				.RegisterCollisionEmitter()
				.Inject(tilemap)
				.Inject(_input)
				.Inject(gridInfo)
				.ProcessInjects()
				.Init();
		}

		void Update()
		{
			_systems?.Run();
		}

		void OnEnable()
		{
			_input.Enable();
		}

		void OnDestroy()
		{
			_systems.Destroy();
			_world.Destroy();
		}
	}
}