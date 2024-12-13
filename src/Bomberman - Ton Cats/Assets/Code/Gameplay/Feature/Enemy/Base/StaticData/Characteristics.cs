using System;

namespace Gameplay.Feature.Enemy.Base.StaticData
{
	[Serializable]
	public struct Characteristics
	{
		public float MovementSpeed;
		
		public int LifePoints => 1;
	}
}