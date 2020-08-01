using Leopotam.Ecs;
using UnityEngine;

namespace RedDev.Game {
    sealed class RocketCollisionProcessing : IEcsRunSystem
    {
	    private const string TAG_BALL = "Ball";
	    private const float REFLECT_SPEED = 10f;
	    private EcsFilter<Rocket,  Collision2DEnterEvent, TransformWrapper> _rocketCollissionsFilter;

        void IEcsRunSystem.Run () {
	        foreach (var i in _rocketCollissionsFilter)
	        {
		        ref var col = ref _rocketCollissionsFilter.Get2(i);
		        var transform = _rocketCollissionsFilter.Get3(i);
				if (col.colliderOther.gameObject.tag == TAG_BALL)
		        {
			        float factor = GetHitFactor(col.colliderOther.transform.position, transform.transform.position, col.colliderOwner.bounds.size.x);
			        Vector2 dir = new Vector2(factor, 1).normalized;
					col.rigidBody.velocity = dir * REFLECT_SPEED;
		        }
	        }
		}

        private float GetHitFactor(Vector2 selfPos, Vector2 racketPos, float racketWidth)
        {
	        return (selfPos.x - racketPos.x) / racketWidth;
        }
	}
}