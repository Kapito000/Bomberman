using System;
using Infrastructure.ECS;

namespace Common.Component
{
	[Serializable]
	public struct Transform
	{
		public UnityEngine.Transform Value;
	}

	public sealed class TransformConverter : Converter<Transform>
	{ }
}