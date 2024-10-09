using System;
using System.Collections.Generic;
using Feature.Hero;
using Infrastructure.SystemFactory;

namespace Feature
{
	public sealed class FeatureController : IDisposable
	{
		readonly List<Infrastructure.Feature> _features = new();

		public FeatureController(ISystemFactory systemFactory)
		{
			Add(systemFactory.Create<HeroFeature>());
		}

		public void Init() =>
			_features.ForEach(f => f.Init());

		public void Update() =>
			_features.ForEach(f => f.Update());

		public void FixedUpdate() =>
			_features.ForEach(f => f.FixedUpdate());

		public void LateUpdate() =>
			_features.ForEach(f => f.LateUpdate());

		public void Dispose()
		{
			_features.ForEach(f => f.Dispose());
			_features.Clear();
		}

		void Add(Infrastructure.Feature feature) => 
			_features.Add(feature);
	}
}