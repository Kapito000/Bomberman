using TMPro;
using UnityEngine;

namespace Common.HUD
{
	public class IntegerDisplay : MonoBehaviour, IDisplay<int>
	{
		[SerializeField] TMP_Text _value;
		
		public void SetValue(int value)
		{
			_value.text = value.ToString();
		}
	}
}