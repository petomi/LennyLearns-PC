using UnityEngine;
using System.Collections;


public class PickUpItem : MonoBehaviour {

	public int itemNumber = 0;

	public AudioSource[] AudioClips = null;

	[HideInInspector]
	bool NearObject = false;
	GameObject correctObject;
	GameObject toDisappear;

	//
	bool nearKaz = false;
	//

	void Update(){

		GameObject Player = GameObject.Find ("Player");
		Inventory inventoryScript = Player.GetComponent<Inventory>();


		if(Input.GetKeyDown (KeyCode.E)){
			if (inventoryScript.inventoryFull != true){ //checks to see if object is already picked up

				if (NearObject == true){
					//if(Input.GetKeyDown(KeyCode.E)){
					Debug.Log ("picked bird up");

					GameObject[] thePickups = GameObject.FindGameObjectsWithTag("PickUp");
					GameObject correctObject = thePickups[itemNumber];
					PickedUpObject objectScript = correctObject.GetComponent<PickedUpObject>();
					objectScript.isPickedUp = true;
	
					toDisappear.GetComponent<SpriteRenderer>().enabled = (false); //hide item instance
	
					//let inventory know which type of object to show
					inventoryScript.isPickUp = true;
					inventoryScript.inventoryFull = true;
					AudioClips[0].Play();
				//}
			}
	//
				if (nearKaz == true){
					//if(Input.GetKeyDown(KeyCode.E)){
					Debug.Log ("mmm burgers");
					GameObject Kaz = GameObject.Find ("kaz burger");
					PickedUpObject kazScript = Kaz.GetComponent<PickedUpObject>();
					kazScript.isPickedUp = true;
					toDisappear.GetComponent<SpriteRenderer>().enabled = (false);

					//letting inventory know which type of object to show (this bool also read by receptacle)
					//	GameObject Player = GameObject.Find ("Player");
					//	Inventory inventoryScript = Player.GetComponent<Inventory>();
					inventoryScript.isKaz = true;
					inventoryScript.inventoryFull = true;
					AudioClips[0].Play();
				}
			//}
			
			}

			else if (inventoryScript.inventoryFull == true){		//displays message if inventory is full. can be repurposed as GUI hook
				Debug.Log ("YOUR INVENTORY IS FULL!!!");
			}
		}

	}


	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "PickUp"){
			NearObject = true;
			toDisappear = other.gameObject;
			//Debug.Log("near object");		//proximity testing
		}

		if(other.tag == "kaz"){
			nearKaz = true;
			toDisappear = other.gameObject;
			
			//Debug.Log("near kaz");	//proximity testing
		}

	}

	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "PickUp"){
			NearObject = false;
			toDisappear = null;

			//Debug.Log ("left object");	//proximity testing
		}

		if (other.tag == "kaz"){
			nearKaz = false;
			toDisappear = null;
			
			//Debug.Log ("left kaz");		//proximity testing
		}	

	}


}