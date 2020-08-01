using Leopotam.Ecs;
using UnityEngine;

public class EcsCollisionBehaviour : MonoBehaviour
{
	[SerializeField]
	protected bool OnCollision2DEnterEnabled;
	[SerializeField]
	protected bool OnCollision2DStayEnabled;
	[SerializeField]
	protected bool OnCollision2DExitEnabled;
	
	public EcsEntity currentEntity;
	
	protected virtual void OnCollisionEnter2D(Collision2D other)
	{
		if (OnCollision2DEnterEnabled)
			Emit<Collision2DEnterEvent>(other);
	}

	protected virtual void OnCollisionStay2D(Collision2D other)
	{
		if (OnCollision2DStayEnabled)
			Emit<Collision2DStayEvent>(other);
	}

	protected virtual void OnCollisionExit2D(Collision2D other)
	{
		if (OnCollision2DExitEnabled)
			Emit<Collision2DExitEvent>(other);
	}

	protected virtual void Emit<T>(Collision2D other) where T: struct, ICollision2DEvent
	{
		if (currentEntity != EcsEntity.Null)
			Collision2DEmitter.GetInstance().Emit<T>(this, other);
		else
			Collision2DEmitter.GetInstance().Emit<T>(other);
	}
}
