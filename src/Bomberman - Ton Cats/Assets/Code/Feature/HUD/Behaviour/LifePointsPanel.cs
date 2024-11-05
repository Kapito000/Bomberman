using TMPro;
using UnityEngine;

namespace Feature.HUD.Behaviour
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