using Infrastructure.ECS;

namespace AI
{
	public interface IAIAgent
	{
		public EntityWrapper Entity { get; }
	}
}