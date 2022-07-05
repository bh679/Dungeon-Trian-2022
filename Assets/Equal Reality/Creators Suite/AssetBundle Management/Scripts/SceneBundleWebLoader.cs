using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;
using UnityEngine.SceneManagement;

[System.Serializable]
public class ContentBundleMeta
{
	public string sceneBundleId;
	public string assetsBundleId;
	public uint version;
	public string appName;
	public string scenarioName;
	public string iconName;
}
	
[System.Serializable]
public class ContentBundleGroup
{
	
	public ContentBundleMeta[] contentBundles;
}

public class SceneBundleWebLoader : MonoBehaviour
{
	static List<AssetBundle> loadedExperienceBundles = new List<AssetBundle>();
	public static AssetBundle iconsBundle;
	
	public const string  bundlePLayerPrefId = "contentBundle";
	public const string rootURL = "https://equalreality.com/AssetBundles/test/"; 
	public const string contentBundleJSONFilename = "contentBundles.txt";
	public const string contentBundleIconsFilename = "contentbundlesicons";
	public string json;
	public bool updateJson = false;//should be readonly getter for public
	//public float loaded = 0;
	
	public ContentBundleGroup contentBundleGroup;
	
	public void ToJson()
	{
		json = JsonUtility.ToJson(contentBundleGroup, true);
	}
	
	void Start()
	{
		StartCoroutine(getJSON());
		StartCoroutine(_LoadIcons());
		
		for(int i = 0; i < loadedExperienceBundles.Count; i++)
		{
			Debug.Log("unloading bundle " + loadedExperienceBundles[i].name);
			loadedExperienceBundles[i].Unload(true);
		}
	}
	
	IEnumerator getJSON()
	{
		updateJson = false;
		
		if(Application.internetReachability == NetworkReachability.NotReachable)
		{
			Debug.Log("No internet, getting local JSON");
			json = PlayerPrefs.GetString(bundlePLayerPrefId);
		}
		else
		{
		
			using(WWW web = new WWW(rootURL+contentBundleJSONFilename))
			{
				yield return web;
		    	
				json = web.text;
				
				PlayerPrefs.SetString(bundlePLayerPrefId,json);
				PlayerPrefs.Save();
			
			}
		}
		contentBundleGroup = JsonUtility.FromJson<ContentBundleGroup>(json);
		
		//loaded = Time.time;
		updateJson = true;
		
	}
	
	IEnumerator _LoadIcons()
	{
		using (UnityWebRequest uwr = UnityWebRequestAssetBundle.GetAssetBundle(rootURL+contentBundleIconsFilename))
		{
			yield return uwr.SendWebRequest();

			if (uwr.result != UnityWebRequest.Result.Success)
			{
				Debug.Log(uwr.error + " "+ rootURL+contentBundleIconsFilename);
			}
			else
			{
				// Get downloaded asset bundle
				iconsBundle = DownloadHandlerAssetBundle.GetContent(uwr);
				//loadedSceneBundles.Add(bundle);
				
	    	
				if(iconsBundle == null)
				{
					Debug.LogError("Faield to download icon asset bundle");
					yield break;
				}
			}
		}
	}
	
	public void LoadBundle(ContentBundleMeta bundleMeta)
	{
		StartCoroutine(_LoadBundleScene(bundleMeta));
	}
	
	bool CheckBundledLoaded(string name)
	{
		
		for(int i = 0; i < loadedExperienceBundles.Count; i++)
		{
			if(loadedExperienceBundles[i].name == name)
				return true;
		}
		
		return false;
	}
	
	IEnumerator _LoadBundleScene(ContentBundleMeta bundleMeta)
	{
		string scenePath = "";
		
		if(!CheckBundledLoaded(bundleMeta.sceneBundleId))
		{
			using (UnityWebRequest uwr = UnityWebRequestAssetBundle.GetAssetBundle(rootURL+bundleMeta.sceneBundleId,bundleMeta.version,0))
			{
				yield return uwr.SendWebRequest();
	
				if (uwr.result != UnityWebRequest.Result.Success)
				{
					Debug.Log(uwr.error);
				}
				else
				{
					
					// Get downloaded asset bundle
					AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(uwr);
					Debug.Log(bundle.name);
					loadedExperienceBundles.Add(bundle);
					
		    	
					if(bundle == null)
					{
						Debug.LogError("Failed to download asset bundle");
						yield break;
					}
		    	
					scenePath = bundle.GetAllScenePaths()[0];
					
					//SceneManager.LoadScene(bundleMeta.sceneName, LoadSceneMode.Single);
					//Instantiate(remoteAssetBundle.LoadAsset(assetName));
					//SceneManager.LoadScene(bundleMeta.sceneName, LoadSceneMode.Single);
		    	
					//bundle.Unload(false);
				}
			}
		}else
		{
			for(int i = 0; i < loadedExperienceBundles.Count; i++)
			{
				if(loadedExperienceBundles[i].name == bundleMeta.sceneBundleId)
					scenePath = loadedExperienceBundles[i].GetAllScenePaths()[0];
			}
		}
			
		if(!CheckBundledLoaded(bundleMeta.sceneBundleId))
		{
			using (UnityWebRequest uwr2 = UnityWebRequestAssetBundle.GetAssetBundle(rootURL+bundleMeta.assetsBundleId,bundleMeta.version,0))
			{
				yield return uwr2.SendWebRequest();

				if (uwr2.result != UnityWebRequest.Result.Success)
				{
					Debug.Log(uwr2.error);
				}
				else
				{
					// Get downloaded asset bundle
					AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(uwr2);
					loadedExperienceBundles.Add(bundle);
				
	    	
					if(bundle == null)
					{
						Debug.LogError("Faield to download asset bundle");
						yield break;
					}
					
					bundle.LoadAllAssets();
	    	
					//Instantiate(remoteAssetBundle.LoadAsset(assetName));
					//SceneManager.LoadScene(bundleMeta.sceneName, LoadSceneMode.Single);
	    	
					//bundle.Unload(false);
				}
			}
		}
		
		
		SceneManager.LoadScene(scenePath);
			
			//Instantiate(remoteAssetBundle.LoadAsset(assetName));
			//SceneManager.LoadScene(bundleMeta.sceneName, LoadSceneMode.Single);
		
	}
}
