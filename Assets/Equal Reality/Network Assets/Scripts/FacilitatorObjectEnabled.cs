using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BNG;

namespace EqualReality.UserModes
{
	
	public class FacilitatorObjectEnabled : MonoBehaviour
	{
	
		public List<Collider> colliders = new List<Collider>();
	
		public List<GameObject> gameObjects = new List<GameObject>();
	
		public List<UnityEngine.UI.Button> buttons = new List<UnityEngine.UI.Button>();
		
		public List<Toggle> toggle = new List<Toggle>();
		public List<Image> images = new List<Image>();
		List<float> imageAlpha = new List<float>();
		public float alpha = 25f;
	
		bool lastMode;
		
		void Start()
		{
			lastMode = Facilitator.mode;
			
			for(int i = 0; i < images.Count; i++)
				imageAlpha.Add(images[i].color.a);
			
			
			for(int i = 0; i < colliders.Count; i++)
				colliders[i].enabled = Facilitator.mode;
				
			for(int i = 0; i < gameObjects.Count; i++)
				gameObjects[i].SetActive(Facilitator.mode);
				
			for(int i = 0; i < toggle.Count; i++)
				toggle[i].interactable = Facilitator.mode;
				
			for(int i = 0; i < buttons.Count; i++)
				buttons[i].interactable = Facilitator.mode;
				
			for(int i = 0; i < images.Count; i++)
				images[i].color = new Color(images[i].color.r,images[i].color.g,images[i].color.b, getAlpha(i));	
		}
	
	    // Update is called once per frame
	    void Update()
		{
			if(lastMode != Facilitator.mode)
			{
				for(int i = 0; i < colliders.Count; i++)
					colliders[i].enabled = Facilitator.mode;
				
				for(int i = 0; i < gameObjects.Count; i++)
					gameObjects[i].SetActive(Facilitator.mode);
				
				for(int i = 0; i < toggle.Count; i++)
					toggle[i].interactable = Facilitator.mode;
				
				for(int i = 0; i < buttons.Count; i++)
					buttons[i].interactable = Facilitator.mode;
				
				for(int i = 0; i < images.Count; i++)
					images[i].color = new Color(images[i].color.r,images[i].color.g,images[i].color.b, getAlpha(i));
			}
			
			lastMode = Facilitator.mode;
		}
	    
		float getAlpha(int i)
		{
			if(Facilitator.mode)
				return imageAlpha[i];
				
			return alpha;
		}
	}
}