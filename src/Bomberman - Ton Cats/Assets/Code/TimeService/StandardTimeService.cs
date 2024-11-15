using UnityEngine;

namespace TimeService
{
	public sealed class StandardTimeService : ITimeService
	{
		public float DeltaTime() => 
			Time.deltaTime;
	}
}