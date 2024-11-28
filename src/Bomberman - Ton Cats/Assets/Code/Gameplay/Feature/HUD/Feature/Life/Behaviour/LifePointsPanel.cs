using TMPro;
using UnityEngine;

namespace Gameplay.Feature.HUD.Feature.Life.Behaviour
{
	public sealed class LifePointsPanel : MonoBehaviour
	{
		[SerializeField] TMP_Text _value;
		
		public void SetValue(int value)
		{
			_value.text = value.ToString();
		}
	}
}