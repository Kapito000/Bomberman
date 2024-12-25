using System.IO;
using UnityEditor.AssetImporters;
using UnityEngine;

namespace StaticTableData.AssetImporters
{
	[ScriptedImporter(1, "tsv")]
	public class TSVImporter : ScriptedImporter
	{
		static bool
			_rememberChoice = false,
			_rememberedChoice = false;

		public override void OnImportAsset(AssetImportContext ctx)
		{
			const char COMMA = ',', DOT = '.';

			var path = ctx.assetPath;
			var fileText = File.ReadAllText(path);
			if (fileText.Contains(COMMA))
			{
				// Auto detect unexpected commas
				_rememberedChoice = true;
				if (!_rememberChoice)
				{
					var variant = UnityEditor.EditorUtility.DisplayDialogComplex(
						"NUMBER FORMAT",
						$"Comma detected in table content at:\n{path}\nWhat should to do?",
						"Replace by dot", "Not replace", "Replace and remember");
					_rememberedChoice = variant != 1;
					_rememberChoice = variant == 2;
				}

				if (_rememberedChoice)
				{
					fileText = fileText.Replace(COMMA, DOT);
					File.WriteAllText(path, fileText);
					UnityEditor.AssetDatabase.Refresh();
					return;
				}
			}
			var textAsset = new TextAsset(fileText);
			ctx.AddObjectToAsset(Path.GetFileNameWithoutExtension(ctx.assetPath),
				textAsset);
			ctx.SetMainObject(textAsset);
		}
	}
}