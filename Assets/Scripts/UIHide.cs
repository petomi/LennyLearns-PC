using UnityEngine;
using System.Collections;

public class UIHide : MonoBehaviour {

	public bool isVisible = false;


	public void Update(){
		CanvasGroup canvasGroup = GetComponent<CanvasGroup>();

		if (isVisible){
			canvasGroup.alpha = 1;
		}

		else{
			canvasGroup.alpha = 0;
		}



	}

}
