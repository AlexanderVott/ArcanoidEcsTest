using System;
using Leopotam.Ecs;
using UnityEngine;

public class BaseCollision2DEmitter<T> where T : struct, ICollision2DEvent
{
	private static BaseCollision2DEmitter<T> _instance;
	protected readonly EcsWorld _world;
	
	protected BaseCollision2DEmitter(EcsWorld world)
	{
		this._world = world;
	}

	public static BaseCollision2DEmitter<T> GetInstance()
	{
		if (_instance?._world == null)
			throw new Exception("Collision2DEmitter not initialize.");

		return _instance;
	}

	internal static BaseCollision2DEmitter<T> Initialize(EcsWorld world)
	{
		if (_instance != null)
			return _instance;

		_instance = new BaseCollision2DEmitter<T>(world);
		return _instance;
	}

	internal static void Finialize()
	{
		_instance = null;
	}

	public virtual EcsEntity Emit(GameObject gameObject, Collision2D collision)
	{
		return EcsEntity.Null;
	}

	public virtual EcsEntity Emit(EcsEntity entity, GameObject gameObject, Collision2D collision)
	{
		return EcsEntity.Null;
	}

	public virtual EcsEntity Emit(EcsEntity entity, Collision2D collision)
	{
		return EcsEntity.Null;
	}
}
