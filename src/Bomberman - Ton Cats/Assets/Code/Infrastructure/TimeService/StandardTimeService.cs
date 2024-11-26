using UnityEngine;

namespace Infrastructure.TimeService
{
	public sealed class StandardTimeService : ITimeService
	{
		public float DeltaTime() => 
			Time.deltaTime;
	}
}