using UnityEngine;
using Menu = Constant.CreateAssetMenu;

namespace StaticData.SceneNames
{
	[CreateAssetMenu(menuName = Menu.Path.c_StaticData + nameof(SceneNamesData),
		fileName = nameof(SceneNamesData))]
	public sealed class SceneNamesData : ScriptableObject, ISceneNameData
	{
		[field: SerializeField] public string Boot { get; private set; }
		[field: SerializeField] public string MainMenu { get; private set; }
		[field: SerializeField] public string Game { get; private set; }
	}
}