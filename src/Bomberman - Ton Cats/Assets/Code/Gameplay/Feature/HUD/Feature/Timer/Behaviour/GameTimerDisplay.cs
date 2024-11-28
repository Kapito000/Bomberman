using System;
using TMPro;
using UnityEngine;

namespace Gameplay.Feature.HUD.Feature.Timer.Behaviour
{
	public class GameTimerDisplay : MonoBehaviour
	{
		[SerializeField] TMP_Text _text;

		public void SetValue(float time)
		{
			var value = TimeSpan.FromSeconds(time).ToString("mm\\:ss");
			_text.text = value;
		}
	}
}