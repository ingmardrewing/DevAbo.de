using UnityEditor;

public class JASScaleFix : AssetPostprocessor { public void OnPreprocessModel() { ModelImporter modelImporter = (ModelImporter) assetImporter; 
		modelImporter.globalScale = 0.01F; 
	} 
}