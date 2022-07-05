using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BundleSceneDownloadButton : MonoBehaviour
{
	SceneBundleWebLoader webLoader;
	ContentBundleMeta bundle;
	
	public Text sceneBundleId;
	public Text assetsBundleId;
	//public string location;
	public Text version;
	public Text sceneName;
	public Text appName;
	public Text scenarioName;
	public Image iconName;
	
	public void SetFromBundle(ContentBundleMeta newBundle, SceneBundleWebLoader _webLoader)
	{
		bundle = newBundle;
		webLoader = _webLoader;
		
		sceneBundleId.text = bundle.sceneBundleId;
		assetsBundleId.text = bundle.assetsBundleId;
		version.text = bundle.version.ToString();
		appName.text = bundle.appName;
		scenarioName.text = bundle.scenarioName;
		if(SceneBundleWebLoader.iconsBundle.Contains(bundle.iconName))
			iconName.sprite = SceneBundleWebLoader.iconsBundle.LoadAsset<Sprite>(bundle.iconName);
		else
			Debug.Log("Cant find icon" + bundle.iconName);
		Debug.Log(bundle.iconName);
			
		Debug.Log(iconName.sprite);
		
	}
	
	public void LoadBundle()
	{
		webLoader.LoadBundle(bundle);
	}
}
