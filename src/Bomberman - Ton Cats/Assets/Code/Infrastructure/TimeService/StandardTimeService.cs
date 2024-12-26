using UnityEngine;

namespace Infrastructure.TimeService
{
	public sealed class StandardTimeService : ITimeService
	{
		public float GameTime() =>
			Time.time;

		public float DeltaTime() =>
			Time.deltaTime;

		public void Stop() =>
			Time.timeScale = 0;

		public void Run() =>
			Time.timeScale = 1;
	}
}