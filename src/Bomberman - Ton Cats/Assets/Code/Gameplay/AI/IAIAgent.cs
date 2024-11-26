using Infrastructure.ECS;

namespace Gameplay.AI
{
	public interface IAIAgent
	{
		public EntityWrapper Entity { get; }
	}
}