using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Inventory : MonoBehaviour {

	[HideInInspector]
	public bool isPickUp = false;
	[HideInInspector]
	public bool isKaz = false;

	public bool inventoryFull = false;

	//add one of these for each pickup. drag and drop pickup into space on script to assign.
	public GameObject birdUI;
	public GameObject pigUI;

	
	void Start () {


	}
	


	void Update () {
		UIHide birdUIHide = birdUI.GetComponent<UIHide>();
		UIHide pigUIHide = pigUI.GetComponent<UIHide>();
		//finds UI objects for pickups (add one line for each new pickup after adding them as UI images into the panel)


		//create one of these for each object
		if(isPickUp){
			birdUIHide.isVisible = true;
		}
		else{
			birdUIHide.isVisible = false;
		}



		if(isKaz){
			pigUIHide.isVisible = true;
		}
		else{
			pigUIHide.isVisible = false;
		}



	}


}
