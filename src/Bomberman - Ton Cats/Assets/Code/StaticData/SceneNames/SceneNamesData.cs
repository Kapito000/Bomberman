using UnityEngine;
using Menu = Constant.CreateAssetMenu;

namespace StaticData.SceneNames
{
	[CreateAssetMenu(
		menuName = Menu.Path.c_SceneNameData + nameof(SceneNamesData),
		fileName = nameof(SceneNamesData))]
	public sealed class SceneNamesData : ScriptableObject, ISceneNameData
	{
		[field: SerializeField] public string BootSceneName { get; private set; }
		[field: SerializeField]
		public string FirstGameSceneName { get; private set; }
	}
}