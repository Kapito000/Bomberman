﻿using Factory;
using UnityEngine;

namespace Feature.HUD.Factory
{
	public interface IHudFactory : IFactory
	{
		int CreateHudRoot(Transform parent);
		void CreateCharacterJoystick(Transform parent);
		void CreatePutBombButton(Transform parent);
		int CreateUpperPanel(Transform parent);
		int CreateLifePointsPanel(Transform parent);
		int CreateBombCounterPanel(Transform parent);
	}
}