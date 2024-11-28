using TMPro;
using UnityEngine;

namespace Gameplay.Feature.HUD.Feature.Bomb.Behaviour
{
	public sealed class BombCounterPanel : MonoBehaviour
	{
		[SerializeField] TMP_Text _value;
		
		public void SetValue(int value)
		{
			_value.text = value.ToString();
		}
	}
}