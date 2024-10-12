using Cinemachine;
using UnityEngine;
using Menu = Constant.CreateAssetMenu;

namespace Infrastructure.AssetProvider
{
	[CreateAssetMenu(
		menuName = Menu.Path.c_AssetProvider + nameof(DirectLinkProvider),
		fileName = nameof(DirectLinkProvider))]
	public sealed class DirectLinkProvider : ScriptableObject, IAssetProvider
	{
		[SerializeField] Camera _camera;
		[SerializeField] CinemachineVirtualCamera _virtualCamera;
		[SerializeField] GameObject _hero;
		[Header("UI")]
		[SerializeField] Canvas _root;

		public Camera Camera() => _camera;
		public CinemachineVirtualCamera VirtualCamera() => _virtualCamera;
		public GameObject Hero() => _hero;
		public Canvas UiRoot() => _root;
	}
}