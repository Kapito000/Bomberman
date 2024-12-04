using System;

namespace Gameplay.Feature.FeatureControl
{
	public interface IFeatureController : IDisposable
	{
		void Init();
		void Start();
		void Update();
		void FixedUpdate();
		void LateUpdate();
		void Cleanup();
	}
}