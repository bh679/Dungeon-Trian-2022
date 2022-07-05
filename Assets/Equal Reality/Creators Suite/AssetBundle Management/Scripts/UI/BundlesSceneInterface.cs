using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BundlesSceneInterface : MonoBehaviour
{
	public SceneBundleWebLoader webLoader;
	public BundleSceneDownloadButton buttonPrefab;
	
	bool created = false;

    // Update is called once per frame
    void Update()
    {
	    if(!created && webLoader.updateJson && SceneBundleWebLoader.iconsBundle != null)
	    {
	    	CreateInterface();
	    }
    }
    
	void CreateInterface()
	{
		created = true;
		int i = 0;
		foreach (ContentBundleMeta bundleMeta in webLoader.contentBundleGroup.contentBundles)
		{
			BundleSceneDownloadButton newButton = Instantiate(buttonPrefab, this.transform);
			newButton.SetFromBundle(bundleMeta, webLoader);
			newButton.transform.localPosition = new Vector3(i*150f,0f,0f);
			
			i++;
		}
	}
}
