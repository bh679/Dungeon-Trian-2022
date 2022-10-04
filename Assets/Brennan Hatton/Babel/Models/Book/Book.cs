using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour
{
	public Animator book, page;
	
	bool isOpen;
	
	public void bookOpen(){
		book.SetTrigger("Open");
		isOpen = true;
	}
	
	public void bookClose(){
		page.gameObject.SetActive(false);
		
		book.SetTrigger("Close");
		isOpen = false;
	}
	
	public void nextPage()
	{
		if(isOpen){
			page.gameObject.SetActive(true);
			page.SetTrigger("nextPage");
		}
	}
	
	public void previousPage()
	{
		if(isOpen){
			page.gameObject.SetActive(true);
			page.SetTrigger("previousPage");
		}
	}
}
