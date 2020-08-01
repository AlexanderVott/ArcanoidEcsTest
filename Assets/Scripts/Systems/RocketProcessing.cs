using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.XR;

namespace RedDev.Game {
    sealed class RocketProcessing : IEcsInitSystem, IEcsRunSystem, IEcsDestroySystem
    {
	    private const string ROCKET_TAG = "Player";

	    private readonly EcsWorld _world = null;
        private EcsFilter<Rocket, InputAxis, RigidBodyComponent, Speed> _inputFilter = null;
        
        public void Init () {
	        foreach (var obj in GameObject.FindGameObjectsWithTag(ROCKET_TAG))
	        {
		        var collision = obj.GetComponent<EcsCollisionBehaviour>();
				var body = obj.GetComponent<Rigidbody2D>();
		        var rocket = _world.NewEntity();
		        rocket
			        .Replace(new Rocket())
			        .Replace(new InputAxis())
			        .Replace(new RigidBodyComponent() {body = body})
			        .Replace(new Speed(){speed = 5f})
			        .Replace(new TransformWrapper() {transform = obj.transform});
		        collision.currentEntity = rocket;

	        }
        }

        public void Run()
        {
	        foreach (var i in _inputFilter)
	        {
		        ref var inputAxis = ref _inputFilter.Get2(i);
		        ref var body = ref _inputFilter.Get3(i);
		        ref var speed = ref _inputFilter.Get4(i);
		        body.body.velocity = inputAxis.axis * speed.speed;
	        }
        }

        public void Destroy()
        {
	        foreach (var i in _inputFilter)
				_inputFilter.GetEntity(i).Destroy();
        }
    }
}