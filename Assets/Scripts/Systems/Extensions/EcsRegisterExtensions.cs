using Leopotam.Ecs;

public static class EcsRegisterExtensions
{
	public static EcsSystems RegisterCollisionEmitter(this EcsSystems systems)
	{
		Collision2DEmitter.Initialize(systems.World);
		InjectOneFrameComponents(systems);
		return systems;
	}

	private static void InjectOneFrameComponents(EcsSystems systems)
	{
		systems
			.OneFrame<Collision2DEnterEvent>()
			.OneFrame<Collision2DStayEvent>()
			.OneFrame<Collision2DExitEvent>();
	}
}
