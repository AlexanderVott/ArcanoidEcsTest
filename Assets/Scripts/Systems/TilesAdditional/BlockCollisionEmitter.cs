using Leopotam.Ecs;

namespace RedDev.Game.Balls
{
	public class BlockCollisionEmitter : BaseCollision2DEmitter<BlockRedBomb>
	{
		protected BlockCollisionEmitter(EcsWorld world) : base(world)
		{
		}
	}
}