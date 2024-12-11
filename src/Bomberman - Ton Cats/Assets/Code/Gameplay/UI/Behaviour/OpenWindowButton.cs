using Gameplay.Windows;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Gameplay.UI.Behaviour
{
	[RequireComponent(typeof(Button))]
	public class OpenWindowButton : MonoBehaviour
	{
		[SerializeField] WindowId _windowId;

		[Inject] IWindowService _windowService;
		
		Button _button;

		void Awake()
		{
			_button = GetComponent<Button>();
			_button.onClick.AddListener(OnButtonClick);
		}

		void OnDestroy()
		{
			_button.onClick.RemoveListener(OnButtonClick);
		}

		void OnButtonClick()
		{
			_windowService.OpenOnly(_windowId);
		}
	}
}