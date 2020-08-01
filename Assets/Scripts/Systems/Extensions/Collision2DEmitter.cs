using System;
using Leopotam.Ecs;
using UnityEngine;

public class Collision2DEmitter
{
	private static Collision2DEmitter _instance;
	private readonly EcsWorld _world;

	private Collision2DEmitter(EcsWorld world)
	{
		this._world = world;
	}

	public static Collision2DEmitter GetInstance()
	{
		if (_instance?._world == null)
		{
			throw new Exception("Collision2DEmitter not initialize.");
		}

		return _instance;
	}

	internal static Collision2DEmitter Initialize(EcsWorld world)
	{
		if (_instance != null)
			return _instance;

		_instance = new Collision2DEmitter(world);
		return _instance;
	}

	public void Emit<T>(Collision2D collision) where T : struct, ICollision2DEvent
	{
		var entity = _world.NewEntity();
		entity.Replace(default(T));
		ref var component = ref entity.Get<T>();
		component.colliderOwner = collision.otherCollider;
		component.colliderOther = collision.collider;
		component.rigidBody = collision.rigidbody;
		component.contactCount = collision.contactCount;
		if (collision.contacts != null)
		{
			component.contacts = new ContactPoint2D[collision.contactCount];
			collision.contacts.CopyTo(component.contacts, 0);
		}
		component.relativeVelocity = collision.relativeVelocity;
	}

	public void Emit<T>(EcsCollisionBehaviour behaviour, Collision2D collision) where T : struct, ICollision2DEvent
	{
		var entity = behaviour.currentEntity;
		if (entity == EcsEntity.Null)
			entity = _world.NewEntity();
		entity.Replace(default(T));
		ref var component = ref entity.Get<T>();
		component.colliderOwner = collision.otherCollider;
		component.colliderOther = collision.collider;
		component.rigidBody = collision.rigidbody;
		component.contactCount = collision.contactCount;
		if (collision.contacts != null)
		{
			component.contacts = new ContactPoint2D[collision.contactCount];
			collision.contacts.CopyTo(component.contacts, 0);
		}
		component.relativeVelocity = collision.relativeVelocity;
	}
}
