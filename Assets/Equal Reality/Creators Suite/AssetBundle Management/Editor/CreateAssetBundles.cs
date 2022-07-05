using UnityEditor;
using UnityEngine;
using System.IO;


public class CreateAssetBundles
{
	
	[MenuItem("Assets/Build All AssetBundles")]
	static void BuildAllAssetBundles()
	{
		string assetbundleDirectory = "Assets/StreamingAssets";
		
		if(!Directory.Exists(Application.streamingAssetsPath))
		{
			Directory.CreateDirectory(assetbundleDirectory);
		}
		
		BuildPipeline.BuildAssetBundles(assetbundleDirectory,BuildAssetBundleOptions.None, EditorUserBuildSettings.activeBuildTarget);	
		
	}
}
