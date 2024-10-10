﻿using System;
using System.Collections.Generic;
using Factory.SystemFactory;
using Feature.Camera;
using Feature.Hero;

namespace Feature
{
	public sealed class FeatureController : IDisposable
	{
		readonly ISystemFactory _systemFactory;
		readonly List<Infrastructure.ECS.Feature> _features = new();

		public FeatureController(ISystemFactory systemFactory)
		{
			_systemFactory = systemFactory;
			Add<HeroFeature>();
			// Add<CameraFeature>();
		}

		public void Init() =>
			_features.ForEach(f => f.Init());

		public void Start() =>
			_features.ForEach(f => f.Start());

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

		void Add(Infrastructure.ECS.Feature feature) =>
			_features.Add(feature);

		void Add<TFeature>() where TFeature : Infrastructure.ECS.Feature =>
			_features.Add(_systemFactory.Create<TFeature>());
	}
}