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
		[SerializeField] GameObject _hero;

		public Camera Camera() => _camera;

		public GameObject Hero() => _hero;
	}
}