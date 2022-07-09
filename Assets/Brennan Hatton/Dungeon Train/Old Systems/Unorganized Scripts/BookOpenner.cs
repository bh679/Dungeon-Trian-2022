using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrennanHatton.DungeonTrain.Book
{

	public class BookOpenner : MonoBehaviour
	{
		public Transform LidSpine;
		public Transform PaperSpine;
		
		public float openSpeed = 250;
		
		public float time = 2;
		public float timeSingle = 0.25f;
		public Transform closeAngle;
		public Transform openAngle;
		public enum PageState
		{
			Openning,
			Turning,
			Open,
			Closed,
			Closing
		}
		PageState pageState = PageState.Closed;
	    
		public void Open()
		{
			if(pageState == PageState.Closed)
				StartCoroutine(openning());
		}
		
		public void Close()
		{
			if(pageState == PageState.Open)
				StartCoroutine(closing());
		}
		
		float timeTaken = 0;
		IEnumerator openning()
		{
			pageState = PageState.Openning;
			float speed = openSpeed* Time.deltaTime;
			float accelerator = 2;
			
			PaperSpine.gameObject.SetActive(true);
			
			while(timeTaken < time)
			{
				LidSpine.rotation = Quaternion.Lerp(closeAngle.rotation, openAngle.rotation, (timeTaken*accelerator)/time);
				
				if(timeTaken/time > 0.5f)
					accelerator -= 0.045f;
				//.localEulerAngles = new Vector3(LidSpine.localEulerAngles.x - speed, LidSpine.localEulerAngles.y, LidSpine.localEulerAngles.z);
				
				/*if( (LidSpine.localEulerAngles.x - openAngle) / openAngle > 0.8f)
				{
					speed -= 0.01f;//1 - (LidSpine.localEulerAngles.x - openAngle) / openAngle * Time.deltaTime;
				}*/
				
				RotatePaper();
				timeTaken += 0.05f;
				yield return new WaitForSeconds(0.05f);
			}
			
			PaperSpine.gameObject.SetActive(false);
			pageState = PageState.Open;
		}
		
		IEnumerator closing()
		{
			pageState = PageState.Closing;
			float speed = openSpeed* Time.deltaTime;
			float accelerator = 2;
			
			PaperSpine.gameObject.SetActive(true);
			
			while(timeTaken < time)
			{
				LidSpine.rotation = Quaternion.Lerp(closeAngle.rotation, openAngle.rotation, (timeTaken*accelerator)/time);
				
				if(timeTaken/time > 0.5f)
					accelerator -= 0.045f;
				//.localEulerAngles = new Vector3(LidSpine.localEulerAngles.x - speed, LidSpine.localEulerAngles.y, LidSpine.localEulerAngles.z);
				
				/*if( (LidSpine.localEulerAngles.x - openAngle) / openAngle > 0.8f)
				{
				speed -= 0.01f;//1 - (LidSpine.localEulerAngles.x - openAngle) / openAngle * Time.deltaTime;
				}*/
				
				RotatePaper();
				timeTaken -= 0.05f;
				yield return new WaitForSeconds(0.05f);
			}
			
			PaperSpine.gameObject.SetActive(false);
			pageState = PageState.Closed;
		}
	    
		public float TurnPage(bool single = false)
		{
			timeTaken = 0;
			if(pageState == PageState.Open)
			{
				StartCoroutine(turningPage(single));
				if(single)
					return timeSingle;
				return time;
			}
			
			return 0;
		}
		
		IEnumerator turningPage(bool single)
		{
			pageState = PageState.Turning;
			float speed = openSpeed* Time.deltaTime;
			float accelerator = 2;
			
			PaperSpine.gameObject.SetActive(true);
			
			float _time = time;
			
			if(single)
				_time = timeSingle;
			
			while(timeTaken < _time)
			{
			
				if(timeTaken/time > 0.5f)
					accelerator -= 0.045f;
				
				RotatePaper();
				
				timeTaken += 0.05f;
				
				yield return new WaitForSeconds(0.05f);
			}
			
			PaperSpine.gameObject.SetActive(false);
			pageState = PageState.Open;
		}
		
		void RotatePaper()
		{
			PaperSpine.rotation = Quaternion.Lerp(closeAngle.rotation, LidSpine.rotation, (timeTaken*25/time) % 1);
			
		}
	}

}