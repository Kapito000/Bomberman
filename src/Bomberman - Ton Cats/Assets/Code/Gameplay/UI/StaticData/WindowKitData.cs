using System.Collections.Generic;
using Gameplay.Windows;
using UnityEngine;
using Menu = Constant.CreateAssetMenu;

namespace Gameplay.UI.StaticData
{
	[CreateAssetMenu(menuName =
		Menu.Path.c_StaticData + nameof(WindowKitData))]
	public sealed class WindowKitData : ScriptableObject, IWindowKitData
	{
		[SerializeField] WindowKitDictionary _kit;

		public bool TryGetKit(WindowKitId kitId, out IReadOnlyList<WindowId> list)
		{
			if (_kit.TryGetValue(kitId, out var kit) == false)
			{
				Debug.LogError($"{nameof(WindowKitId)} has no \"{kitId}\".");
				list = default;
				return false;
			}

			list = kit.Values;
			return true;
		}
	}
}