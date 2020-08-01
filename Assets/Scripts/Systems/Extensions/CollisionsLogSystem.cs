using Leopotam.Ecs;
using UnityEngine;

namespace RedDev.Game {
    sealed class CollisionsLogSystem : IEcsInitSystem, IEcsRunSystem
    {
	    private EcsFilter<Collision2DEnterEvent> filterEnterCollision2D;
	    private EcsFilter<Collision2DStayEvent> filterStayCollision2D;
	    private EcsFilter<Collision2DExitEvent> filterExitCollision2D;
        
        public void Init () {
#if UNITY_EDITOR
            Debug.Log("Collision logger inited. Don't forget please remove this in build");
#endif
        }

        public void Run()
        {
#if UNITY_EDITOR
	        ForEachCollision2D(filterEnterCollision2D);
	        ForEachCollision2D(filterStayCollision2D);
	        ForEachCollision2D(filterExitCollision2D);
#endif
        }

        private void ForEachCollision2D<T>(EcsFilter<T> filter) where T : struct, ICollision2DEvent
        {
	        foreach (var i in filter)
	        {
		        ref var happened = ref filter.Get1(i);
                Log($"Collision event {typeof(T).Name}", happened.colliderOwner.gameObject, happened.colliderOther.gameObject);
	        }
        }

        private void Log(string type, GameObject from, GameObject to)
        {
	        Debug.Log($"[LogCollisionsSystems] {type} {from.name} with ${to.name}");
        }
    }
}