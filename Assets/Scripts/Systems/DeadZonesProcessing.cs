using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RedDev.Game {
    sealed class DeadZonesProcessing : IEcsInitSystem, IEcsRunSystem
    {
	    private const string DEADZONES_TAG = "DeadZone";
	    private const string BALL_TAG = "Ball";

        readonly EcsWorld _world = null;

        private EcsFilter<DeadZoneEvent, Collision2DEnterEvent> _deadZonesFilter = null;

        public void Init()
        {
	        foreach (var obj in GameObject.FindGameObjectsWithTag(DEADZONES_TAG))
	        {
		        var collisionWrapper = obj.GetComponent<EcsCollisionBehaviour>();
		        var deadzone = _world.NewEntity();
		        deadzone
			        .Replace(new DeadZoneEvent());
                if (collisionWrapper != null)
					collisionWrapper.currentEntity = deadzone;
	        }
        }

        void IEcsRunSystem.Run () {
	        foreach (var i in _deadZonesFilter)
	        {
		        var collider = _deadZonesFilter.Get2(i);
		        if (collider.colliderOther.gameObject.tag == BALL_TAG)
		        {
			        SceneManager.LoadScene(0);
					break;
		        }
	        }
        }
    }
}